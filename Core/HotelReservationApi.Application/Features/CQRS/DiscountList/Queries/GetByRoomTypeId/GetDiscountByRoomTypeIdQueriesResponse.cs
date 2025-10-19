using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetByRoomTypeId
{
    public class GetDiscountByRoomTypeIdQueriesResponse
    {
        public string DiscountCategoryName { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsGlobal { get; set; }
        public DateTime? DiscountStartTime { get; set; }
        public DateTime? DiscountEndTime { get; set; }
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set; }
        public int? StayDays { get; set; }
        public int? PayDays { get; set; }
    }
}
