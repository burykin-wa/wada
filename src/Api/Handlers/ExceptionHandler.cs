using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionHandler(IHostEnvironment environment, ILogger<ExceptionHandler> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            if (exception is AuthServiceException authServiceException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.ContentType = "application/problem+json";
                var problemDetail = CreateProblemDetails(httpContext, authServiceException);
                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problemDetail);

                return true;
            }
            else if (exception is ServiceException serviceException)
            {
                httpContext.Response.StatusCode = serviceException.StatusCode;
                httpContext.Response.ContentType = "application/problem+json";
                var problemDetail = CreateProblemDetails(httpContext, serviceException);
                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problemDetail);

                return true;
            }           
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/problem+json";
                var problemDetail = CreateProblemDetails(httpContext, exception);
                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problemDetail);

                return true;
            }
        }

        private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            var statusCode = context.Response.StatusCode;
            var message = exception.Message;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = message,
            };

            if (_environment.IsDevelopment())
            {

                problemDetails.Detail = exception.ToString();
                problemDetails.Extensions["traceId"] = context.TraceIdentifier;
                problemDetails.Extensions["data"] = exception.Data;
            }

            return problemDetails;
        }
    }
}
