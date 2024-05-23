using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SampleExceptionHandler.Payload.Response;

namespace SampleExceptionHandler.ExceptionHandler{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger){
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"exception: {exception.Message}");
            var errorResponse = new Response
            {
                Title = "Exception",
                Detail = exception.Message
            };
            switch (exception)
            {
                case BadHttpRequestException:
                    errorResponse.Status = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name;
                    break;
                case CustomValidationException validationException:
                    errorResponse.Status = (int) HttpStatusCode.BadRequest;
                    errorResponse.Title = "Invalid Data Request";
                    errorResponse.Errors = validationException.Errors;
                    break;
                case System.ComponentModel.DataAnnotations.ValidationException validationException:
                    errorResponse.Status = (int) HttpStatusCode.BadRequest;
                    errorResponse.Title = "Invalid Data Request";
                    errorResponse.Errors = (IDictionary<string, string[]>?)validationException.Data;
                    break;
                default:
                    errorResponse.Status = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "Internal Server Error";
                    errorResponse.Detail = "Unexpected Error!";
                    break;
            }
            httpContext.Response.StatusCode = errorResponse.Status??500;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
            return true;
        }
    }
}