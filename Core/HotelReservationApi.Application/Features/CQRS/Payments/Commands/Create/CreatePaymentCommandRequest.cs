using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Payments.Commands.Create
{
    public class CreatePaymentCommandRequest : IRequest
    {
        public string PaymentToken { get; set; }
        public int PaymentTimingId { get; set; }
        public int PaymentMethodId { get; set; }

    }
}
