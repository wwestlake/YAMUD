using FluentResults;
using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.WebAPI.Services;

public interface IUserAccountService
{
    Task<Result<IEnumerable<UserAccount>>> GetAllUserAccounts();
    Task<Result<UserAccount?>> GetUserAccountById(Guid id);
    Task<Result<UserAccount?>> GetUserAccountByEmail(string email);
    Task<Result<UserAccount?>> CreateUserAccount(CreateUserModel userAccount);
    Task<Result> UpdateUserAccount(Guid id, UserAccount updatedUserAccount);
    Task<Result> DeleteUserAccount(Guid id);
    Task<Result> VerifyUserEmail(Guid userId, Guid verificationToken);
    Task<Result<string>> AuthenticateAsync(string email, string password);
    Task<Result<UserAccount>> GetCurrentUser();
}
