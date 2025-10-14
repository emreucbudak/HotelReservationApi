using HotelReservationApi.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationApi.Presentation.ExceptionHandler
{
    public class NotFoundExceptionsHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionsHandler> _logger;
        private readonly IProblemDetailsService _problemDetailsService;

        public NotFoundExceptionsHandler(ILogger<NotFoundExceptionsHandler> logger, IProblemDetailsService problemDetailsService)
        {
            _logger = logger;
            _problemDetailsService = problemDetailsService;
        }

        public  ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if ( exception is not NotFoundExceptions)
                return ValueTask.FromResult(false);
            _logger.LogError("Bir hata oluştu: {Message}", exception.Message);
            var problemDetails = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = new ProblemDetails
                {
                    Title = "Bulunamadı!",
                    Detail = exception.Message,
                    Status = StatusCodes.Status404NotFound,

                }
                
            };
            return _problemDetailsService.TryWriteAsync(problemDetails);
   

        }
    }
}
