using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Exceptions
{
    public class HotelAdressNotFoundExceptions : NotFoundExceptions
    {
        public HotelAdressNotFoundExceptions(int id) : base($"{id}'e sahip otelin adres bilgisi bulunamadı!")
        {
        }
    }
}
