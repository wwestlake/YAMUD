using FluentEmail.Core;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public class YamudFluentEmailFactory : IFluentEmailFactory
    {
        public IFluentEmail Create()
        {
            return new Email();
        }
    }
}
