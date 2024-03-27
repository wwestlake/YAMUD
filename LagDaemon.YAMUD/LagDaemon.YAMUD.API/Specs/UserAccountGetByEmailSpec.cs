using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.User;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.API.Specs
{
    public class UserAccountGetByEmailSpec : IQuerySpec<UserAccount>
    {
        private string _email;

        public UserAccountGetByEmailSpec(string email)
        {
            _email = email;
        }


        public Expression<Func<UserAccount, bool>> Filter => u => u.EmailAddress == _email;
        public Func<IQueryable<UserAccount>, IOrderedQueryable<UserAccount>> OrderBy => null;
        public string IncludeProperties => "PlayerState,UserRoles";
    }
}
