using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Coupon  : BaseEntity
    {
        public string CouponCode { get; set; }
        public int DiscountPercentage { get; set; }
        public int MaxUsageCount { get; set; }
        public int CurrentUsageCount { get; set; } = 0;
    }
}
