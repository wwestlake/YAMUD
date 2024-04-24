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
            if (!IsCurrentUserAuthorized(securityAttribute.Roles))
            {
                throw new ApplicationException("Not Authorized");
            }
        }

        // Call the underlying implementation
        invocation.Proceed();
    }

    // Example method to simulate user role check
    private bool IsCurrentUserAuthorized(UserAccountRoles[] roleNames)
    {
        //if (_requestContext.Roles == null || !_requestContext.Roles.Any())
        //{
        //    return false;
        //}
        // RULE: A Users role must be <= a role specified in the roles from  the method
        // Found > Owner > Admin > Modeator > PLayer

        // Map the role names to their corresponding integer values (assuming UserAccountRoles is an enum)
        var roleValues = roleNames.Select(r => (int)r);

        // Get the lowest role value specified in the array (This is the highest ranking role)
        var highestRoleValue = roleValues.Min();

        // Check if the user's highest role is less than or equal to the highest role specified in the array
        return _requestContext.Roles.Any(role => (int)role <= highestRoleValue);
    }
}
