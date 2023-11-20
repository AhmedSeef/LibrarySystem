using LibrarySystem.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;

namespace LibrarySystem.Server.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleException(context, error);
            }
        }

        private async Task HandleException(HttpContext context, Exception error)
        {
            _logger.LogError(error, "Error Handler Middleware");

            var response = context.Response;
            response.ContentType = "application/json";

            string message = GetErrorMessage(error);

            response.StatusCode = GetStatusCode(error);

            var result = System.Text.Json.JsonSerializer.Serialize(new { ErrorMessage = message });
            await response.WriteAsync(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(result)), Encoding.UTF8);
        }

        private static string GetErrorMessage(Exception error)
        {
            switch (error)
            {
                case NotFoundException:
                case KeyNotFoundException:
                    return "Key Not Found Exception";
                case RepetitionException:
                    return "The name is already saved before";
                default:
                    return "Internal Server Error";
            }
        }

        private static int GetStatusCode(Exception error)
        {
            switch (error)
            {
                case NotFoundException:
                case KeyNotFoundException:
                    return (int)HttpStatusCode.NotFound;
                case RepetitionException:
                    return (int)HttpStatusCode.Conflict;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
