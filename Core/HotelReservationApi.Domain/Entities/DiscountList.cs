using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class DiscountList : BaseEntity
    {
        public DiscountList()
        {
            
        }

        public DiscountList(int discountPercentage, DateTime? discountStartTime, DateTime? discountEndTime, DateTime? bookingStartDate, DateTime? bookingEndDate, bool ısGlobal, int? stayDays, int? payDays, int? hotelsId, int? roomTypeId)
        {

            DiscountPercentage = discountPercentage;

            DiscountStartTime = discountStartTime;
            DiscountEndTime = discountEndTime;
            BookingStartDate = bookingStartDate;
            BookingEndDate = bookingEndDate;
            IsGlobal = ısGlobal;
            StayDays = stayDays;
            PayDays = payDays;
            HotelsId = hotelsId;
            RoomTypeId = roomTypeId;
        }
        public int DiscountCategoryId { get; set; }
        public DiscountCategory DiscountCategory { get; set; }
   
        public int DiscountPercentage { get; set; }
        public bool IsGlobal { get; set; }
        public DateTime? DiscountStartTime { get; set; }
        public DateTime? DiscountEndTime { get; set; }
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set; }
        public int? StayDays { get; set; } 
        public int? PayDays { get; set; }
        public int? HotelsId {  get; set; }
        public Hotels Hotels { get; set; }

        public int? RoomTypeId { get; set; }
        public RoomTypes RoomType { get; set; }

    }
}
