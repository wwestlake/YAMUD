using System;

namespace LagDaemon.YAMUD.API.Security
{
    public class ServiceProxyFactory : IServiceProxyFactory
    {
        private readonly ISecurityProxyFactory _securityProxyFactory;

        public ServiceProxyFactory(ISecurityProxyFactory securityProxyFactory)
        {
            _securityProxyFactory = securityProxyFactory;
        }

        public T CreateProxy<T>(T underlyingService) where T : class
        {
            // Create and return the proxy using the ISecurityProxyFactory
            return _securityProxyFactory.CreateProxy<T>(underlyingService);
        }
    }
}
