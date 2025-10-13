using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Exceptions
{
    public class ReviewsNotFoundExceptions : NotFoundExceptions
    {
        public ReviewsNotFoundExceptions(int id) : base($"{id}'e sahip yorum bulunamadı")
        {
        }
    }
}
