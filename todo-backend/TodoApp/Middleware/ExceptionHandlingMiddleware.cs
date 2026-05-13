using System.Net;
using System.Text.Json;

namespace TodoApp.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred!");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex) 
        {
            context.Response.ContentType = "application/json";
            var response = new { message = ex.Message, detailed = "An error occurred while processing your request!" };
            switch (ex)
            {
                case KeyNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new { message = e.Message, detailed = "Resource not found!" }; break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; break;
            }
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
