using LagDaemon.YAMUD.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public static class UserExtensions
    {
        public static int MaxRole(this IEnumerable<UserRole> roles)
        {
            return roles.Select(r => (int)r.Role).Max();
        }
    }
}
