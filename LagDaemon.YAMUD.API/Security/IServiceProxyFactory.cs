using System;

namespace LagDaemon.YAMUD.API.Security
{
    public interface IServiceProxyFactory
    {
        T CreateProxy<T>(T underlyingService) where T : class;
    }
}
