using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Behaviors
{
    public  class LoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggerBehavior<TRequest,TResponse>> _log;

        public LoggerBehavior(ILogger<LoggerBehavior<TRequest, TResponse>> log)
        {
            _log = log;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var req = JsonSerializer.Serialize(request);
            _log.LogInformation("------ Gelen İstek: {RequestName} veri ile birlikte: {@Request}", requestName, req);
            var response = await next();
            var res = JsonSerializer.Serialize(response);
            _log.LogInformation("------ Giden Cevap: {RequestName} veri ile birlikte: {@Response}", requestName, res);
            return response;
        }
    }
}
