using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.TwoFactors
{
    public class TwoFactorsAuthCommandRequest : IRequest<TwoFactorsAuthCommandResponse>
    {
        public string Code { get; set; }
        public string? TempToken { get; set; }
    }
}
