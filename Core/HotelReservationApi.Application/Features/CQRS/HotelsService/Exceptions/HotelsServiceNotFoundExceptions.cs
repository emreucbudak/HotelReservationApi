using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Exceptions
{
    public class HotelsServiceNotFoundExceptions : NotFoundExceptions
    {
        public HotelsServiceNotFoundExceptions(int id) : base($"{id}'e sahip servis bulunamadı!")
        {
        }
    }
}
