using Castle.DynamicProxy;
using System;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace LagDaemon.YAMUD.API.Security
{
    public class SecurityProxyFactory : ISecurityProxyFactory
    {
        private readonly ISecurityInterceptor _securityInterceptor;

        public SecurityProxyFactory(ISecurityInterceptor securityInterceptor)
        {
            _securityInterceptor = securityInterceptor ?? throw new ArgumentNullException(nameof(securityInterceptor));
        }

        public T CreateProxy<T>(T underlyingService) where T : class
        {
            var generator = new ProxyGenerator();
            return generator.CreateInterfaceProxyWithTarget<T>(underlyingService, _securityInterceptor);
        }
    }
}
