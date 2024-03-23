﻿using FluentResults;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services.LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Services;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LagDaemon.YAMUD.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserAccount> _userRepository;
        private IEmailService _emailService;

        public UserAccountService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = _unitOfWork.GetRepository<UserAccount>();
            _emailService = emailService;
        }

        public Result<IEnumerable<UserAccount>> GetAllUserAccounts()
        {
            return Result.Ok(_userRepository.GetAll());
        }

        public Result<UserAccount> GetUserAccountById(Guid id)
        {
            return Result.Ok(_userRepository.GetById(id));
        }

        public Result<UserAccount> GetUserAccountByEmail(string email)
        {
            Expression<Func<UserAccount, bool>> filter = u => u.EmailAddress == email;
            return Result.Ok(_userRepository.GetSingle(filter));
        }

        public Result<UserAccount> CreateUserAccount(UserAccount userAccount)
        {
            var validationResult = ValidateUserAccount(userAccount);
            if (validationResult.IsFailed)
            {
                return validationResult;
            }

            userAccount.HashedPassword = HashPassword(userAccount.HashedPassword);
            _userRepository.Insert(userAccount);
            _unitOfWork.SaveChanges();
            return Result.Ok(userAccount);
        }

        public Result UpdateUserAccount(Guid id, UserAccount updatedUserAccount)
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
        }

        public Result DeleteUserAccount(Guid id)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                return Result.Fail($"User with ID '{id}' not found.");
            }

            _userRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return Result.Ok();
        }

        private Result SendEmailVerification(UserAccount userAccount)
        {
            userAccount.VerificationToken = Guid.NewGuid();
            UpdateUserAccount(userAccount.ID, userAccount);

            var viewModel = new AccountVerificationViewModel()
            {
                DisplayName = userAccount.DisplayName,
                VerificationUrl = $"https://localhost/"
            };

            return Result.Ok();
        }

        private Result ValidateUserAccount(UserAccount userAccount)
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

            return errors.Count > 0 ? Result.Fail(errors) : Result.Ok();
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
    }
}
