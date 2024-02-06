using Microsoft.AspNetCore.Diagnostics;

namespace SimpleBookCatalog.Api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            var errorDetails = new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            };

            await httpContext.Response.WriteAsync(errorDetails.ToString(), cancellationToken: cancellationToken);

            return true;
        }
    }
}
