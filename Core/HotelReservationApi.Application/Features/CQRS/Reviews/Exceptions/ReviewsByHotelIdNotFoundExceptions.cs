using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Exceptions
{
    public class ReviewsByHotelIdNotFoundExceptions : NotFoundExceptions
    {
        public ReviewsByHotelIdNotFoundExceptions(int id) : base($"{id}'e ait otelin yorumları bulunamadı!")
        {
        }
    }
}
