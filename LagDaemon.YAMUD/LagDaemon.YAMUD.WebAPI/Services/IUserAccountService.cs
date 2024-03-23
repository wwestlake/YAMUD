using FluentResults;
using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public interface IUserAccountService
    {
        Result<IEnumerable<UserAccount>> GetAllUserAccounts();
        Result<UserAccount> GetUserAccountById(string id);
        Result<UserAccount> GetUserAccountByEmail(string id);
        Result<UserAccount> CreateUserAccount(UserAccount userAccount);
        Result UpdateUserAccount(string id, UserAccount updatedUserAccount);
        Result DeleteUserAccount(string id);
    }
}
