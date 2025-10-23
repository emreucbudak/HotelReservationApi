using HotelReservationApi.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
