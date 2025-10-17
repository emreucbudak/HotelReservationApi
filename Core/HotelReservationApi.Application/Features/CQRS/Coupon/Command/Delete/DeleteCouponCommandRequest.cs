using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Command.Delete
{
    public class DeleteCouponCommandRequest : IRequest
    {
        public int Id { get; set; }

        public DeleteCouponCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
