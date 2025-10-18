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
        public Rooms()
        {
        }

        public Rooms(int roomNumber, bool ısAvailable, int priceListId, int roomTypesId, int hotelsId)
        {
            RoomNumber = roomNumber;
            IsAvailable = ısAvailable;
            PriceListId = priceListId;
            RoomTypesId = roomTypesId;
            HotelsId = hotelsId;
        }

        public int RoomTypesId { get; set; }
        public RoomTypes RoomTypes { get; set; }
        public int RoomNumber { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }

    }
}
