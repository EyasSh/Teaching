using System;
using Microsoft.AspNetCore.Http;
namespace Middleware.User.Security
{
  
interface IAsyncMiddleware
{
    public Task InvokeAsync(HttpContext context);
}
    public class UserSecurity:IAsyncMiddleware
    {
       private readonly RequestDelegate _next;
        public UserSecurity()
        {
        
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
