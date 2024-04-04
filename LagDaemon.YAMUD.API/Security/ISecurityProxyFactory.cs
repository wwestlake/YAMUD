using System;

namespace LagDaemon.YAMUD.API.Security
{
    public interface ISecurityProxyFactory
    {
        T CreateProxy<T>(T underlyingService) where T : class;
    }
}
