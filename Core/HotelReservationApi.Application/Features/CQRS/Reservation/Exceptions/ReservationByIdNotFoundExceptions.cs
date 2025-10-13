using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Exceptions
{
    public class ReservationByIdNotFoundExceptions : NotFoundExceptions
    {
        public ReservationByIdNotFoundExceptions(int id) : base($"{id}'e sahip otele ait  rezervasyon bilgisi bulunamadı!")
        {
        }
    }
    {
    }
}
