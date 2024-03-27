using Castle.DynamicProxy;
using LagDaemon.YAMUD.Model;
using System.Reflection;

namespace LagDaemon.YAMUD.API.Security;


public class SecurityInterceptor : ISecurityInterceptor
{
    private readonly IRequestContext _requestContext;

    public SecurityInterceptor(IRequestContext requestContext)
    {
        _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext));
    }


    public void Intercept(IInvocation invocation)
    {
        var name = invocation.Method.Name;

        var securityAttribute = invocation.InvocationTarget.GetType().GetMethod(name).GetCustomAttribute<SecurityAttribute>();
        if (securityAttribute != null)
        {
            // Perform security check based on the security attribute

            // For demonstration purposes, assume access is granted if the user has the "Admin" role
            if (!CurrentUserHasRole(securityAttribute.Roles))
            {
                throw new ApplicationException("Not Authorized");
            }
        }

        // Call the underlying implementation
        invocation.Proceed();
    }

    // Example method to simulate user role check
    private bool CurrentUserHasRole(UserAccountRoles[] roleNames)
    {
        return _requestContext.Roles.Any( role => roleNames.Contains(role) );
    }
}
