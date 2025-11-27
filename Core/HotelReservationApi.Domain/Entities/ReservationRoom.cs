using HotelReservationApi.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationApi.Domain.Entities
{
    [Index(nameof(RoomId), nameof(ReservationId))]
    public class ReservationRoom : BaseEntity
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int RoomId { get; set; }
        public Rooms Room { get; set; }
    }
}
