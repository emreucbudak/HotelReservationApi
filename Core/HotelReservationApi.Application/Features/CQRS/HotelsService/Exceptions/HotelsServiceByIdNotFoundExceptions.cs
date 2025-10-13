using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Exceptions
{
    public class HotelsServiceByIdNotFoundExceptions : NotFoundExceptions
    {
        public HotelsServiceByIdNotFoundExceptions(int id) : base($"{id}'e sahip hotelin servisleri bulunamadı!")
        {
        }
    }
}
