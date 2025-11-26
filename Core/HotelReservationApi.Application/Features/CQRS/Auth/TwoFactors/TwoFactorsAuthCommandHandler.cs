using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.TwoFactors
{
    public class TwoFactorsAuthCommandHandler : IRequestHandler<TwoFactorsAuthCommandRequest, TwoFactorsAuthCommandResponse>
    {
        public Task<TwoFactorsAuthCommandResponse> Handle(TwoFactorsAuthCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
