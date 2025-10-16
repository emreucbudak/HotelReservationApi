using HotelReservationApi.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace HotelReservationApi.Presentation.ExceptionHandler
{
    public sealed class AuthExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _prds;

        private readonly ILogger<AuthException> _log;
        public AuthExceptionHandler(IProblemDetailsService prds, ILogger<AuthException> log)
        {
            _prds = prds;
            _log = log;
        }


        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not AuthException authException)
            {
                return ValueTask.FromResult(false);
            }
            _log.LogWarning("Bir hata oluştu! Hata : {Error}", exception.Message);
            var context = new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new()
                {
                    Title = "Bir hata oluştu",
                    Detail = exception.Message,
                    Status = StatusCodes.Status401Unauthorized,
                }
            };
            return _prds.TryWriteAsync(context);
        }
    }
}
