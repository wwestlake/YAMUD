using FluentResults;
using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public interface IUserAccountService
    {
        Result<IEnumerable<UserAccount>> GetAllUserAccounts();
        Result<UserAccount> GetUserAccountById(Guid id);
        Result<UserAccount> GetUserAccountByEmail(string email);
        Result<UserAccount> CreateUserAccount(UserAccount userAccount);
        Result UpdateUserAccount(Guid id, UserAccount updatedUserAccount);
        Result DeleteUserAccount(Guid id);
    }
}
