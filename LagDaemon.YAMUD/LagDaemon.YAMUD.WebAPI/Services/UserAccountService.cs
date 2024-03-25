using FluentResults;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Models;
using LagDaemon.YAMUD.WebAPI.Services;
using LagDaemon.YAMUD.WebAPI.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LagDaemon.YAMUD.Services;

public class UserAccountService : IUserAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<UserAccount> _userRepository;
    private IEmailService _emailService;
    private RazorViewToStringRenderer _razorViewToStringRenderer;
    private IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccountService(IUnitOfWork unitOfWork, IEmailService emailService, 
                            RazorViewToStringRenderer razorViewToStringRenderer, 
                            IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userRepository = _unitOfWork.GetRepository<UserAccount>();
        _emailService = emailService;
        _razorViewToStringRenderer = razorViewToStringRenderer;
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<IEnumerable<UserAccount>>> GetAllUserAccounts()
    {
        return await Task.Run(() => {
            return Result.Ok(_userRepository.GetAll());
        });
    }

    public async Task<Result<UserAccount>> GetUserAccountById(Guid id)
    {
        return Result.Ok(_userRepository.GetById(id));
    }

    public async Task<Result<UserAccount>> GetUserAccountByEmail(string email)
    {
        return await Task.Run(() =>
        {
            Expression<Func<UserAccount, bool>> filter = u => u.EmailAddress == email;
            return Result.Ok(_userRepository.GetSingle(filter));
        });        }

    public async Task<Result<UserAccount>> CreateUserAccount(CreateUserModel userAccount)
    {
        return await Task.Run(async () =>
        {
            var newUserAccount = new UserAccount() 
            { 
                DisplayName = userAccount.DisplayName,
                EmailAddress = userAccount.Email,
                HashedPassword = userAccount.Password,
            };

            var validationResult = await ValidateUserAccount(newUserAccount);
            if (validationResult.IsFailed)
            {
                return validationResult;
            }

            newUserAccount.HashedPassword = HashPassword(newUserAccount.HashedPassword);
            newUserAccount.PlayerState.UserAccountId = newUserAccount.ID;
            newUserAccount.PlayerState.UserAccount = newUserAccount;
            _userRepository.Insert(newUserAccount);
            _unitOfWork.SaveChanges();

            return Result.Ok(newUserAccount);
        });
    }

    public async Task<Result> UpdateUserAccount(Guid id, UserAccount updatedUserAccount)
    {
        return await Task.Run(() =>
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                return Result.Fail($"User with ID '{id}' not found.");
            }

            updatedUserAccount.ID = id;
            _userRepository.Update(updatedUserAccount);
            _unitOfWork.SaveChanges();
            return Result.Ok();
        });
    }

    public async Task<Result> DeleteUserAccount(Guid id)
    {
        return await Task.Run(() =>
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                return Result.Fail($"User with ID '{id}' not found.");
            }

            _userRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return Result.Ok();
        });
    }

    private async Task<string> GetHostName()
    {
        return await Task.Run(() =>
        {
            // Access the current HTTP context and retrieve the host name
            var host = $"{_httpContextAccessor.HttpContext.Request.Host}";

            // Return the host name as a string
            return host.ToString();
        });
    }

    private async Task<Result> SendEmailVerification(UserAccount userAccount)
    {
        userAccount.VerificationToken = Guid.NewGuid();
        await UpdateUserAccount(userAccount.ID, userAccount); // Assuming this method updates the user account in the database

        // TODO: get host from environment
        var viewModel = new AccountVerificationViewModel()
        {
            DisplayName = userAccount.DisplayName,
            VerificationUrl = $"https://{GetHostName()}/api/UserAccount/VerifyEmail/{userAccount.ID}/{userAccount.VerificationToken.ToString()}", // Replace this with your actual verification URL
            Token = userAccount.VerificationToken.ToString(),
        };

        // Send the email
        var emailResult = await _emailService.SendEmailAsync(userAccount.EmailAddress, "Account Verification", "AccountVerification", viewModel);

        if (emailResult.IsSuccess)
        {
            return Result.Ok();
        }
        else
        {
            // Handle the case where sending the email fails
            return Result.Fail("Failed to send email verification.");
        }
    }


    private async Task<Result> ValidateUserAccount(UserAccount userAccount)
    {
        return await Task.Run(() =>
        {
            var errors = new List<string>();

            if (_userRepository.GetSingle(u => u.EmailAddress == userAccount.EmailAddress) != null)
            {
                errors.Add($"Email address '{userAccount.EmailAddress}' is already in use.");
            }

            if (!ValidateEmail(userAccount.EmailAddress))
            {
                errors.Add("Email address is not valid.");
            }

            double passwordEntropy = CalculatePasswordEntropy(userAccount.HashedPassword);
            if (passwordEntropy < 80)
            {
                errors.Add($"Password strength must be greater than 80, but is {passwordEntropy}.");
            }

            return errors.Count > 0 ? Result.Fail(errors) : SendEmailVerification(userAccount).Result;
        });
    }

    private bool ValidateEmail(string email)
    {
        string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    private double CalculatePasswordEntropy(string password)
    {
        // Define the character set (e.g., lowercase letters, uppercase letters, digits, special characters)
        string charSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";

        // Calculate the total possible combinations based on the character set and password length
        double combinations = Math.Pow(charSet.Length, password.Length);

        // Calculate the entropy in bits
        double entropyBits = Math.Log(combinations, 2);

        // Return entropy as a floating-point value
        return entropyBits;
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public async Task<Result> VerifyUserEmail(Guid userId, Guid verificationToken)
    {
        var result = Result.Ok();

        var userAccount = await GetUserAccountById(userId);

        userAccount.OnSuccess( async acc => { 
            if (acc.VerificationToken == verificationToken)
            {
                acc.VerificationToken = Guid.Empty;
                acc.Status = UserAccountStatus.Verified;
                await UpdateUserAccount(userId, acc);
            }
        } ).OnFailure( msg => {
            result = Result.Fail(msg);
        } );

        return result;
    }

    public string GenerateJwtToken(UserAccount userAccount)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somekindofsecretkey1234567890!@#$%^&*()-_=+"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userAccount.EmailAddress),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            // Add more claims as needed
        };

        var token = new JwtSecurityToken(
            issuer: "yamud.lagdaemon.com",
            audience: "localhost",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24), // Token expiration time
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Result<string>> AuthenticateAsync(string email, string password)
    {
        Result<string> result = Result.Ok("Verified");

        var userResult = await GetUserAccountByEmail(email);
        userResult.OnSuccess(user => {
            if (user == null || !VerifyPassword(password, user.HashedPassword))
            {
                 result = Result.Fail<string>("Invalid email or password");
            } else
            {
                result = Result.Ok(GenerateJwtToken(user));
            }
        }).OnFailure(x => { 
            result = Result.Fail(x);
        });

        return result;
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = Convert.FromBase64String(hashedPassword);
            byte[] inputBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Compare the hashed password stored in the database with the hashed input password
            return hashedBytes.SequenceEqual(inputBytes);
        }
    }
}
