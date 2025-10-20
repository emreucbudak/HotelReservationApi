using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Command.Create
{
    public class CreateDiscountListCommandRequest : IRequest
    {

        public int DiscountPercentage { get; set; }
        public bool IsGlobal { get; set; }
        public DateTime? DiscountStartTime { get; set; }
        public DateTime? DiscountEndTime { get; set; }
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set; }
        public int DiscountCategoryId { get; set; }
        public int? RoomTypeId { get; set; }
        public int? StayDays { get; set; }
        public int? PayDays { get; set; }
        public int? HotelsId { get; set; }
        public bool IsDiscountForOnlyRoomTypes { get; set; }
    }
}
