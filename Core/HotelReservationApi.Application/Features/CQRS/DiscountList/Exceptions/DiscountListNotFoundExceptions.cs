using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Exceptions
{
    public class DiscountListNotFoundExceptions : NotFoundExceptions
    {
        public DiscountListNotFoundExceptions(int id) : base($"{id}'e sahip indirim bulunamadı!" )
        {
        }
    }
}
