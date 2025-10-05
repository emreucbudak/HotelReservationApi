using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetByName
{
    public class GetCouponByNameQueriesResponse
    {
        public string CouponCode { get; set; }
        public int DiscountPercentage { get; set; }
        public int MaxUsageCount { get; set; }
        public int CurrentUsageCount { get; set; }
    }
}
