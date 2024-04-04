using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.User;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.API.Specs
{
    public class UserAccountGetByIdSpec : IQuerySpec<UserAccount>
    {
        private Guid _findId;

        public UserAccountGetByIdSpec(Guid findId)
        {
            _findId = findId;
        }


        public Expression<Func<UserAccount, bool>> Filter => u => u.ID == _findId;
        public Func<IQueryable<UserAccount>, IOrderedQueryable<UserAccount>> OrderBy => null;
        public string IncludeProperties => "PlayerState,UserRoles";
    }
}
