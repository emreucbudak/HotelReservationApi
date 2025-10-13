using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Exceptions
{
    public class RoomsGetByIdNotFoundExceptions : NotFoundExceptions
    {
        public RoomsGetByIdNotFoundExceptions(int id) : base($"{id}'e sahip otele ait oda bulunamadı!")
        {
        }
    }
}
