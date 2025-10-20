using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Exceptions
{
    public class CouponUsageReachLimitExceptions : UsageReachLimitExceptions
    {
        public CouponUsageReachLimitExceptions(string couponCode) : base($"{couponCode} koduna ait kodun kullanım limiti dolmuştur")
        {
        }
    }
}
