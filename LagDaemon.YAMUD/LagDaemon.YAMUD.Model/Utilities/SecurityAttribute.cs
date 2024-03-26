using LagDaemon.YAMUD.Model;
using System;
using System.Data;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class SecurityAttribute : Attribute
{
    public SecurityAttribute(params UserAccountRoles[] roles)
    {
        Roles = roles;
    }

    public UserAccountRoles[] Roles { get; }
}
