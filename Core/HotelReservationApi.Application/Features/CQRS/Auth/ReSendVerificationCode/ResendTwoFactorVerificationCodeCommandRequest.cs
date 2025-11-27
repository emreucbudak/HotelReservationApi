using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.ReSendVerificationCode
{
    public class ResendTwoFactorVerificationCodeCommandRequest : IRequest
    {
        public string TempToken { get; set; }
        }
}
