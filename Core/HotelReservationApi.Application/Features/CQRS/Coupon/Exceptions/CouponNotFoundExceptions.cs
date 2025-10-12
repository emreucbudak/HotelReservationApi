using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Exceptions
{
    public class CouponNotFoundExceptions : NotFoundExceptions
    {
        public CouponNotFoundExceptions(int id) : base($"{id}'e sahip kupon bulunamadı!")
        {
        }
    }
}
