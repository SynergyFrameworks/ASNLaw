using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Scion.Infrastructure.Web.Middleware
{
    public class ApiErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiErrorWrappingMiddleware> _logger;

        public ApiErrorWrappingMiddleware(RequestDelegate next, ILogger<ApiErrorWrappingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                //Need handle only storefront api errors
                if (!context.Response.HasStarted && context.Request.Path.ToString().Contains("/api/"))
                {
                    var message = $@"An exception occurred while processing the request [{context.Request.Path}]: {ex} ";
                    _logger.LogError(ex, message);
                    var httpStatusCode = HttpStatusCode.InternalServerError;
                    var json = JsonConvert.SerializeObject(new { message, stackTrace = ex.StackTrace });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)httpStatusCode;
                    await context.Response.WriteAsync(json);
                }
                else
                {
                    //Continue default error handling
                    throw;
                }
            }
        }
    }
}
