using LibrarySystem.Shared.Exceptions;
using System.Net;
using System.Text;

namespace LibrarySystem.Server.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private static string message;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
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
                case FluentValidation.ValidationException e:
                    StringBuilder stringBuilderValidationException = new StringBuilder("Validation Exception");
                    foreach (var property in e.Errors)
                    {
                        stringBuilderValidationException.AppendLine(property.PropertyName);
                        stringBuilderValidationException.Append(": ");
                        stringBuilderValidationException.Append(property.ErrorMessage);
                    }
                    message = stringBuilderValidationException.ToString();
                    return message;
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
                case FluentValidation.ValidationException:
                    return (int)HttpStatusCode.BadRequest;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
