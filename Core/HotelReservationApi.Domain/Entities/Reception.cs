using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class Reception : BaseEntity
    {
        public Reception()
        {
        }

        public Reception(User userId, int hotelsId)
        {
            this.User = userId;
            HotelsId = hotelsId;
        }

        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
