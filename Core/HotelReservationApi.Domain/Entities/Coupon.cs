using HotelReservationApi.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationApi.Domain.Entities
{
    public class Coupon  : BaseEntity
    {
        public Coupon()
        {
        }

        public Coupon(string couponCode, int discountPercentage, int maxUsageCount )
        {
            CouponCode = couponCode;
            DiscountPercentage = discountPercentage;
            MaxUsageCount = maxUsageCount;
            CurrentUsageCount =  0;
        }

        public string CouponCode { get; set; }
        public int DiscountPercentage { get; set; }
        public DateOnly ExpireDate { get; set; }
        public int MaxUsageCount { get; set; }
        public int CurrentUsageCount { get; set; } = 0;
        [Timestamp]
        [Column("xmin", TypeName = "xid")]
        public uint ConcurrencyToken { get; set; }



    }
}
