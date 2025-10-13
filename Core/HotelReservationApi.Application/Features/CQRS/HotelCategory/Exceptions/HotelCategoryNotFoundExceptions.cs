using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelCategory.Exceptions
{
    public class HotelCategoryNotFoundExceptions : NotFoundExceptions
    {
        public HotelCategoryNotFoundExceptions(int id) : base($"{id}'e sahip hotelcategory eklendi")
        {
        }
    }
}
