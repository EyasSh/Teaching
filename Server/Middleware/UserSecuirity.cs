using System;
using Microsoft.AspNetCore.Mvc;
namespace Middleware.User.Security
{
  

    public class UserSecurity:ControllerBase
    {
       private readonly RequestDelegate _next;
        public UserSecurity(RequestDelegate rd)
        {
          _next = rd;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var message = context.Request.PathBase.ToString();
            Console.WriteLine(message);
            await _next(context);
        }
    }
    public static class UserSecExtension
    {
        public static IApplicationBuilder UseUserSecurity(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserSecurity>();
        }
    }
}
