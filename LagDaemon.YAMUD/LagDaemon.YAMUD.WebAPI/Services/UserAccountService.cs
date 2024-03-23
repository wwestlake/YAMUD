using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using FluentResults;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Services;

namespace LagDaemon.YAMUD.Services
{
    public class UserAccountService : IUserAccountService
    {

        /// <summary>
        /// Create a user account service with a MongoClient
        /// </summary>
        /// <param name="mongoClient">Injected by DI</param>
        public UserAccountService()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a list of all user accounts.  This may take some time
        /// if the list is long.  
        /// </summary>
        /// <returns>IEnumerable<UserAccount></returns>
        public Result<IEnumerable<UserAccount>> GetAllUserAccounts()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a user account based on the ID
        /// </summary>
        /// <param name="id">The ID of the User (Internal string of Guid)</param>
        /// <returns>Success -> UserAccount, Fail -> Error Message</returns>
        public Result<UserAccount> GetUserAccountById(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a user account based on Email.  Email addresses must
        /// be unique in the system
        /// </summary>
        /// <param name="email">The email address of the user account</param>
        /// <returns>Result Success -> UserAccount, Fail -> Errpr Message</returns>
        public Result<UserAccount> GetUserAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a user account.
        /// </summary>
        /// <param name="userAccount">The user acocunt DTO</param>
        /// <returns>A UserAccount or Errors</returns>
        public Result<UserAccount> CreateUserAccount(UserAccount userAccount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a user acocunt
        /// </summary>
        /// <param name="id">The account to update</param>
        /// <param name="updatedUserAccount">the updated account</param>
        /// <returns>OK or Errors</returns>
        public Result UpdateUserAccount(string id, UserAccount updatedUserAccount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a User Account
        /// </summary>
        /// <param name="id">ID of User Acocunt (Guid as string)</param>
        /// <returns>Ok or Errors</returns>
        public Result DeleteUserAccount(string id)
        {
            throw new NotImplementedException();
        }

        private  Result<UserAccount> ProcessUserAccount(UserAccount userAccount)
        {
            var isFailed = false;
            var errors = new List<string>();

            var existing = GetUserAccountByEmail(userAccount.EmailAddress);
            if (existing.IsSuccess)
            {
                errors.Add($"Email Address '{userAccount.EmailAddress}' already exists");
                isFailed = true;
            }

            if (! ValidateEmail(userAccount.EmailAddress))
            {
                errors.Add("Email address is not valid");
                isFailed = true;
            }

            double passwordEntropy = CalculatePasswordEntropy(userAccount.HashedPassword);
            if (passwordEntropy < 80) 
            {
                errors.Add($"Password strength must be > 80, but is {passwordEntropy}");
            }

            if (isFailed)
            {
                return Result.Fail(errors);
            }
            else
            {
                userAccount.HashedPassword = HashPassword(userAccount.HashedPassword);
                return userAccount;
            }
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
