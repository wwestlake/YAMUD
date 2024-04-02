using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Services;

namespace LagDaemon.YAMUD.WebAPI.Models
{
    public abstract class DataMask<TDto, TDao>
        where TDto : class
        where TDao : class
    {
        public abstract TDto Map(TDao dao);
        public abstract Task<TDao?> Map(TDto dto);
    }

    public class UserAccountMask : DataMask<UserAccountDto, UserAccount>
    {
        private IUserAccountService _userAccountService;

        public UserAccountMask(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public override UserAccountDto Map(UserAccount dao)
        {  //roleNames.Select(r => (int)r);
            return new UserAccountDto()
            {
                Id = dao.ID,
                DisplayName = dao.DisplayName,
                Email = dao.EmailAddress,
                Role = (dao.UserRoles.Select(r => r.Role).Min()).ToString()
            };
        }

        public override async Task<UserAccount?> Map(UserAccountDto dto)
        {
            return (await _userAccountService.GetUserAccountById(dto.Id)).ValueOrDefault;
        }
    }


}
