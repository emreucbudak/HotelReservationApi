using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Rooms : BaseEntity
    {
        public int RoomTypesId { get; set; }
        public RoomTypes RoomTypes { get; set; }
        public int RoomNumber { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
