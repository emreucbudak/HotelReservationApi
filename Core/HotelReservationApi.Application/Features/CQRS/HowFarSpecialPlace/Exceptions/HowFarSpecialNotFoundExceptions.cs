using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Exceptions
{
    public class HowFarSpecialNotFoundExceptions : NotFoundExceptions
    {
        public HowFarSpecialNotFoundExceptions(int id) : base($"{id}'e ait özel konum bulunamadı!")
        {
        }
    }
}
