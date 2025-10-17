using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Exceptions
{
    public class HotelsNotFoundExceptions : NotFoundExceptions
    {
        public HotelsNotFoundExceptions(int id) : base($"{id}'e sahip hotel bulunamadı!")
        {
        }
    }
}
