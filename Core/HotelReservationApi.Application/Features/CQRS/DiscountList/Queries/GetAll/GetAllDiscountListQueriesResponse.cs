using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetAll
{
    public class GetAllDiscountListQueriesResponse
    {
        public int Id { get; set; }
        public bool IsDiscountActive { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsDiscountForReservationDate { get; set; }
        public DateTime? DiscountStartTime { get; set; }
        public DateTime? DiscountEndTime { get; set; }
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set; }
        public int? StayDays { get; set; }
        public int? PayDays { get; set; }
        public string? HotelName { get; set; }
        public string? TypeName { get; set; }
    }
}
