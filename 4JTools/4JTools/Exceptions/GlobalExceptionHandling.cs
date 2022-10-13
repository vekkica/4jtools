using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace _4JTools.Exceptions
{
    public static class GlobalExceptionHandling
    {
        public static async Task HandleAsync(HttpContext ctx)
        {
            ctx.Response.ContentType = "application/json";

            var ex = ctx.Features.Get<IExceptionHandlerFeature>();

            if (ex is { Error: { } })
            {
                var msg = ex.Error.InnerException?.Message ?? ex.Error.Message;

                ctx.Response.StatusCode = ex.Error switch
                {
                    EntityNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.BadRequest,
                };

                await ctx.Response.WriteAsync(JsonSerializer.Serialize(new { Error = msg }));
            }
        }
    }
}
