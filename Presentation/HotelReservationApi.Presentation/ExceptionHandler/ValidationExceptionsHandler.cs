using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace HotelReservationApi.Presentation.ExceptionHandler
{
    public class ValidationExceptionsHandler : IExceptionHandler
    {
        private readonly ILogger<ValidationExceptionsHandler> _logger;
        private readonly IProblemDetailsService _prds;



        public ValidationExceptionsHandler(ILogger<ValidationExceptionsHandler> logger, IProblemDetailsService prds)
        {
            _logger = logger;
            _prds = prds;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validationException)
                return ValueTask.FromResult(false);
            _logger.LogError("Bir hata ile karşılaşıldı = {error.message}",exception.Message);
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var context = new ProblemDetailsContext() { HttpContext = httpContext,
                ProblemDetails = new()
                {
                    Title = "Doğrulama hatası",
                    Detail = exception.Message,
                    Status = StatusCodes.Status400BadRequest
                }
            
            };
            var error = validationException.Errors
                .GroupBy(x=> x.PropertyName)
                .ToDictionary(g=> g.Key, g => g.ToArray());
            context.ProblemDetails.Extensions.Add("err", error);
            return _prds.TryWriteAsync(context);




        }
    }
}
