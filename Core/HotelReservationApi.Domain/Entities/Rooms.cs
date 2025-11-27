using HotelReservationApi.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationApi.Domain.Entities
{
    [Index(nameof(RoomTypesId))]
    public class Rooms : BaseEntity
    {
        public Rooms()
        {
        }

        public Rooms(int roomNumber, int roomTypesId)
        {
            RoomNumber = roomNumber;
            
           
            RoomTypesId = roomTypesId;
            reservationRooms = new List<ReservationRoom>();

           
        }

        public int RoomTypesId { get; set; }
        public RoomTypes RoomTypes { get; set; }
        public int RoomNumber { get; set; }
        public ICollection<ReservationRoom>? reservationRooms {  get; set; }


    }
}
