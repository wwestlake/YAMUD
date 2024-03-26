using System.Security.Claims;

namespace LagDaemon.YAMUD.WebAPI.Services
{
    public class RequestInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRequestContext requestContext)
        {
            // Extract information from the HTTP request (e.g., user ID)
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;



            // Store information in the request context
            requestContext.UserEmail = userId;

            await _next(context);
        }
    }
}
