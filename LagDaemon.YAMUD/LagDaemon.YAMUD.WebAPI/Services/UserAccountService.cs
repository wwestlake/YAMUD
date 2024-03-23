using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using FluentResults;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Services;
using MongoDB.Driver;

namespace LagDaemon.YAMUD.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMongoCollection<UserAccount> _userAccountCollection;

        public UserAccountService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("yamud");
            _userAccountCollection = database.GetCollection<UserAccount>("users");
        }

        public Result<IEnumerable<UserAccount>> GetAllUserAccounts()
        {
            return _userAccountCollection.Find(_ => true).ToList();
        }

        public Result<UserAccount> GetUserAccount(Guid id)
        {
            var filter = Builders<UserAccount>.Filter.Eq(u => u.ID, id);
            return _userAccountCollection.Find(filter).FirstOrDefault();
        }

        public Result<UserAccount> CreateUserAccount(UserAccount userAccount)
        {
            var processed = ProcessUserAccount(userAccount);
            Result<UserAccount> result = null;

            processed.OnSuccess(x =>
            {
                _userAccountCollection.InsertOne(userAccount);
                result = Result.Ok(userAccount);
            }).OnFailure(x =>
            {
                result = Result.Fail(x);
            });
            return result;
        }

        public void UpdateUserAccount(Guid id, UserAccount updatedUserAccount)
        {
            var filter = Builders<UserAccount>.Filter.Eq(u => u.ID, id);
            _userAccountCollection.ReplaceOne(filter, updatedUserAccount);
        }

        public void DeleteUserAccount(Guid id)
        {
            var filter = Builders<UserAccount>.Filter.Eq(u => u.ID, id);
            _userAccountCollection.DeleteOne(filter);
        }

        private  Result<UserAccount> ProcessUserAccount(UserAccount userAccount)
        {
            var isFailed = false;
            var errors = new List<string>();

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

            userAccount.HashedPassword = HashPassword(userAccount.HashedPassword);

            return userAccount;
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
