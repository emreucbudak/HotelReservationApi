using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelServices : BaseEntity
    {
        public HotelServices()
        {
        }

        public HotelServices(int serviceId, int hotelsId)
        {
            ServiceId = serviceId;
            HotelsId = hotelsId;
        }

        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }
       public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
