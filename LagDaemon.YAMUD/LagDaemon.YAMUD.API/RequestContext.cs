using LagDaemon.YAMUD.Model;

namespace LagDaemon.YAMUD.API;

public class RequestContext : IRequestContext
{
    public string UserEmail { get; set; }
    public IEnumerable<UserAccountRoles> Roles { get; set; }
}
