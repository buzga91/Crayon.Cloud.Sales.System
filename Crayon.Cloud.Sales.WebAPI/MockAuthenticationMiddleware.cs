using System.Security.Claims;

namespace Crayon.Cloud.Sales.WebAPI
{
    public class MockAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public MockAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User?.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                // Create a ClaimsPrincipal with a mocked CustomerId claim
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "mock-user"),
                new Claim("CustomerId", "2"), // Mocked CustomerId
            };

                var identity = new ClaimsIdentity(claims, "mock");
                context.User = new ClaimsPrincipal(identity);
            }

            await _next(context);
        }
    }
}
