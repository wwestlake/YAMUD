using FluentEmail.Core;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Data.Repositories;
using LagDaemon.YAMUD.Model;
using LagDaemon.YAMUD.Model.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public class RequestInfoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public RequestInfoMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context, IRequestContext requestContext)
        {
            // Extract information from the HTTP request (e.g., user ID)
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Resolve scoped services using the service locator pattern
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var repo = unitOfWork.GetRepository<UserAccount>();

                var user = repo.Get().AsQueryable().Include(u => u.UserRoles).FirstOrDefault(user => user.EmailAddress == userId);

                if (user == null)
                {
                    requestContext.UserEmail = string.Empty;
                    requestContext.Roles = new List<UserAccountRoles>();
                }
                else
                {
                    // Store information in the request context
                    requestContext.UserEmail = userId;
                    requestContext.Roles = user.UserRoles.Select(x => x.Role);
                }
            }
            await _next(context);
        }
    }
}
