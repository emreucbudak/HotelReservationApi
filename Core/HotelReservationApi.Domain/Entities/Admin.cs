using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class Admin : BaseEntity
    {
        public Admin(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; } 
        public User User { get; set; }
    }
}
