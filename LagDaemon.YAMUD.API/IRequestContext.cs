using LagDaemon.YAMUD.Model;

namespace LagDaemon.YAMUD.API;

public interface IRequestContext
{
    string UserEmail { get; set; }

    IEnumerable<UserAccountRoles> Roles { get; set; }
}
