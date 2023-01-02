using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Clay.Tests.Api
{
    class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim("Id", "123"),
                new Claim(ClaimTypes.Name, "Test user"),
                new Claim(ClaimTypes.Role, "Admin")
            }));

            await next();
        }
    }
}
