using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelManager : BaseEntity
    {
        public HotelManager()
        {
        }

        public HotelManager(int hotelsId, User user)
        {
            HotelsId = hotelsId;
            this.User = user;
        }

        public  int HotelsId { get; set; }
        public Hotels Hotels { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
