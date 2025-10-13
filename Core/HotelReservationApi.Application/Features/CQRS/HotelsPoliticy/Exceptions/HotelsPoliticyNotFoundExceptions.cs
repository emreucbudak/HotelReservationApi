using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Exceptions
{
    public class HotelsPoliticyNotFoundExceptions : NotFoundExceptions
    {
        public HotelsPoliticyNotFoundExceptions(int id) : base($"{id}'e sahip hotel politakası bulunamadı!")
        {
        }
    }
}
