using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.API.Specs;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Model;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using LagDaemon.YAMUD.API;

namespace LagDaemon.YAMUD.WebAPI.Services.ChatServices
{
    public class TokenValidationFilter : IHubFilter
    {
        IRequestContext _requestContext;
        private IServiceProvider _serviceProvider;

        public TokenValidationFilter(IRequestContext context, IServiceProvider serviceProvider)
        {
            _requestContext = context;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeMethodAsync(HubCallerContext context, Func<Task> next)
        {
            var httpRequest = context.GetHttpContext();
            var token = httpRequest.Request.Headers["Authorization"]; // Example: Assuming token in Authorization header
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var repo = unitOfWork.GetRepository<UserAccount>();

                var user = (await repo.GetAsync(new UserAccountGeneralQuerySpec())).FirstOrDefault();

                if (user == null)
                {
                    _requestContext.UserEmail = string.Empty;
                    _requestContext.Roles = new List<UserAccountRoles>();
                }
                else
                {
                    // Store information in the request context
                    _requestContext.UserEmail = userId;
                    _requestContext.Roles = user.UserRoles.Select(x => x.Role);
                }
            }

            await next();  // Continue processing the SignalR method call
        }
    }
}
