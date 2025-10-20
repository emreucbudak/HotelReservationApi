using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Command.Update
{
    public class UpdateCouponCommandRequest : IRequest
    {
        public string CouponCode { get; set; }

        public UpdateCouponCommandRequest(string couponCode)
        {
            CouponCode = couponCode;
        }
    }
}
