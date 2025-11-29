using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Payments.Exception
{
    public class PaymentFailedExceptions : FailedExceptions
    {
        public PaymentFailedExceptions() : base("Ödeme İşlemi")
        {
        }
    }
}
