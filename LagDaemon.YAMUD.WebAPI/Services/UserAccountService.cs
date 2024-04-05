using FluentResults;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.API.Specs;
using LagDaemon.YAMUD.Model;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Model.Utilities;
using LagDaemon.YAMUD.WebAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
    private readonly IRequestContext _requestContext;
    private readonly ILogger _logger;

    public UserAccountService(IUnitOfWork unitOfWork, IEmailService emailService, 
                            RazorViewToStringRenderer razorViewToStringRenderer, 
                            IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor,
                            IRequestContext requestContext, ILogger<UserAccountService> logger
                            )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userRepository = _unitOfWork.GetRepository<UserAccount>();
        _emailService = emailService;
        _razorViewToStringRenderer = razorViewToStringRenderer;
        _env = env;
        _httpContextAccessor = httpContextAccessor;
        _requestContext = requestContext;
        _logger = logger;
    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result<IEnumerable<UserAccount>>> GetAllUserAccounts()
    {
        var userList = await _userRepository.GetAsync(new UserAccountGeneralQuerySpec());
        return Result.Ok(userList);
    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result<UserAccount?>> GetUserAccountById(Guid id)
    {
        var existingUser = (await _userRepository.GetAsync(new UserAccountGetByIdSpec(id))).FirstOrDefault();
        if (await CompareRoles(_requestContext.UserEmail, existingUser))
        {
            return Result.Ok(existingUser);
        }
        return Result.Ok(existingUser);
    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result<UserAccount?>> GetUserAccountByEmail(string email)
    {
        var existingUser = (await _userRepository.GetAsync(new UserAccountGetByEmailSpec(email))).FirstOrDefault();
        if (await CompareRoles(_requestContext.UserEmail, existingUser))
        {
            return Result.Ok(existingUser);
        }
        return Result.Fail("Not Authorized");
    }

    public async Task<Result<UserAccount?>> CreateUserAccount(CreateUserModel userAccount)
    {
        UserAccount? newUserAccount = new() 
        { 
            DisplayName = userAccount.DisplayName,
            EmailAddress = userAccount.Email,
            HashedPassword = userAccount.Password,
            UserRoles = new List<UserRole>() {}
        };

        if (await _userRepository.CountAsync() == 0)
        {
            newUserAccount.UserRoles.Add(
                new UserRole()
                {
                    Role = UserAccountRoles.Founder,
                    User = newUserAccount,
                    UserId = newUserAccount.ID
                });
        } else
        {
            newUserAccount.UserRoles.Add(
                new UserRole()
                {
                    Role = UserAccountRoles.Player,
                    User = newUserAccount,
                    UserId = newUserAccount.ID
                });
        }


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
    }


    [Security(UserAccountRoles.Admin)]
    public async Task<Result> UpdateUserAccount(Guid id, UserAccount updatedUserAccount)
    {
        var existingUser = _userRepository.GetById(id);
        if (existingUser == null)
        {
            return Result.Fail($"User with ID '{id}' not found.");
        }

        if (await CompareRoles(_requestContext.UserEmail, existingUser))
        {
            updatedUserAccount.ID = id;
            _userRepository.Update(updatedUserAccount);
            _unitOfWork.SaveChanges();
        }
        return Result.Ok();
    }

    [Security(UserAccountRoles.Player)]
    public async Task<Result<UserAccount?>> GetCurrentUser()
    {
        return (await _userRepository.GetAsync(u => u.EmailAddress == _requestContext.UserEmail, null, "UserRoles")).FirstOrDefault();
    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result> DeleteUserAccount(Guid id)
    {
        var existingUser = _userRepository.GetById(id);
        if (existingUser == null)
        {
            return Result.Fail($"User with ID '{id}' not found.");
        }
        if (await CompareRoles(_requestContext.UserEmail, existingUser))
        {
            _userRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }
        return Result.Ok();
    }

    private async Task<bool> CompareRoles(string actorEmail, UserAccount target)
    {
        var actor = (await _userRepository.GetAsync(new UserAccountGetByEmailSpec(actorEmail))).FirstOrDefault();
        if (actor.ID == target.ID)
        {
            return true;
        }
        var roleValues = actor.UserRoles.Select(r => (int)r.Role);
        var actorRole = roleValues.Max();

        var userRoles = target.UserRoles.Select(r => (int)r.Role);
        var maxUserRole = userRoles.Max();

        return actorRole > maxUserRole;
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

        var viewModel = new AccountVerificationViewModel()
        {
            DisplayName = userAccount.DisplayName,
            VerificationUrl = $"https://{await GetHostName()}/api/UserAccount/VerifyEmail/{userAccount.ID}/{userAccount.VerificationToken.ToString()}", // Replace this with your actual verification URL
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

        var userAccount = (await _userRepository.GetAsync(new UserAccountGetByIdSpec(userId) )).FirstOrDefault();

        if (userAccount.VerificationToken == verificationToken)
        {
            userAccount.VerificationToken = Guid.Empty;
            userAccount.Status = UserAccountStatus.Verified;
            _userRepository.Update(userAccount);
            _unitOfWork.SaveChanges();
        }

        return result;
    }

    // TODO: Fix key security HERE Get key from environment
    public string GenerateJwtToken(UserAccount userAccount)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somekindofsecretkey1234567890!@#$%^&*()-_=+"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userAccount.EmailAddress),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // Add more claims as needed
            new Claim(ClaimTypes.Role, userAccount.UserRoles.MaxUserAccountRole().ToString()),
            new Claim(ClaimTypes.Name, userAccount.DisplayName)
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
        var userResult = (await _userRepository.GetAsync(new UserAccountGetByEmailSpec(email))).FirstOrDefault();
        if (userResult == null || !VerifyPassword(password, userResult.HashedPassword))
        {
                return Result.Fail<string>("Invalid email or password");
        } else
        {
            _logger.LogInformation($"User {userResult.EmailAddress} has logged in.");
            return Result.Ok(GenerateJwtToken(userResult));
        }
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
