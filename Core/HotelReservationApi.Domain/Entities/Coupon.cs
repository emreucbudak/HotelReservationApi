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
        public Coupon()
        {
        }

        public Coupon(string couponCode, int discountPercentage, int maxUsageCount, int? currentUsageCount)
        {
            CouponCode = couponCode;
            DiscountPercentage = discountPercentage;
            MaxUsageCount = maxUsageCount;
            CurrentUsageCount = currentUsageCount ?? 0;
        }

        public string CouponCode { get; set; }
        public int DiscountPercentage { get; set; }
        public int MaxUsageCount { get; set; }
        public int CurrentUsageCount { get; set; }
    }
}
