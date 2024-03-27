using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Model.Utilities;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.API.Specs
{
    public class UserAccountGeneralQuerySpec : IQuerySpec<UserAccount>
    {
        public Expression<Func<UserAccount, bool>> Filter => u => true;
        public Func<IQueryable<UserAccount>, IOrderedQueryable<UserAccount>> OrderBy => null;
        public string IncludeProperties => "PlayerState,UserRoles";
    }

    public class UserAccountRoleBasedQuerySpec : IQuerySpec<UserAccount>
    {
        private int _maxRoleOfrCaller;

        public UserAccountRoleBasedQuerySpec(UserAccountRoles maxRoleOfCaller)
        {
            _maxRoleOfrCaller = (int)maxRoleOfCaller;
        }

        public Expression<Func<UserAccount, bool>> Filter => u => u.UserRoles.MaxRole() <= _maxRoleOfrCaller;
        public Func<IQueryable<UserAccount>, IOrderedQueryable<UserAccount>> OrderBy => null;
        public string IncludeProperties => "PlayerState,UserRoles";
    }

}
