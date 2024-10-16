using LawUI.Exceptions;
using System.Net;
using System.Text.Json;

namespace LawUI;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, errorMessage) = exception switch
        {
            AzureConfigurationException => (HttpStatusCode.ServiceUnavailable, "Azure Configuration Error", exception.Message),
            ArgumentException => (HttpStatusCode.BadRequest, "Invalid Argument", exception.Message),
            FileNotFoundException => (HttpStatusCode.NotFound, "File Not Found", exception.Message),
            UnauthorizedAccessException => (HttpStatusCode.Forbidden, "Unauthorized Access", exception.Message),
            DatabaseConnectionException => (HttpStatusCode.ServiceUnavailable, "Database Connection Error", exception.Message),
            _ => (HttpStatusCode.InternalServerError, "Unexpected Error", "An unexpected error occurred while processing your request.")
        };

        context.Response.StatusCode = (int)statusCode;

        // Check if the request is an API call or a page request
        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
            context.Request.Path.StartsWithSegments("/api"))
        {
            // For API calls, return JSON response
            context.Response.ContentType = "application/json";
            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Title = title,
                Message = errorMessage,
                RequestId = context.TraceIdentifier
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        else
        {
            // For page requests, redirect to the error page
            var encodedErrorMessage = System.Web.HttpUtility.UrlEncode($"{title}: {errorMessage}");
            var redirectUrl = $"/Error?message={encodedErrorMessage}&statusCode={context.Response.StatusCode}&requestId={context.TraceIdentifier}";
            context.Response.Redirect(redirectUrl);
            return Task.CompletedTask;
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class GlobalErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}

// Custom exception for database connection errors
public class DatabaseConnectionException : Exception
{
    public DatabaseConnectionException(string message) : base(message) { }
    public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException) { }
}