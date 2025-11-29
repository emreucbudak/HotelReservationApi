using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Payments.Exception
{
    public class ReservationNotFoundExceptions : NotFoundExceptions
    {
        public ReservationNotFoundExceptions() : base("Ödeme yapılmak istenen rezervasyon bulunamadı!")
        {
        }
    }
}
