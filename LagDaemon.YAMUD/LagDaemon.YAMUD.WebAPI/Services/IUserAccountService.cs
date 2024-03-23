using FluentResults;
using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public interface IUserAccountService
    {
        Result<IEnumerable<UserAccount>> GetAllUserAccounts();
        Result<UserAccount> GetUserAccount(Guid id);
        Result<UserAccount> CreateUserAccount(UserAccount userAccount);
        void UpdateUserAccount(Guid id, UserAccount updatedUserAccount);
        void DeleteUserAccount(Guid id);
    }
}
