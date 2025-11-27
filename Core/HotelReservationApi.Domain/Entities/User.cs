using Microsoft.AspNetCore.Identity;

namespace HotelReservationApi.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationTime { get; set; }
        public Admin Admin { get; set; }
        public HotelManager HotelManager { get; set; }
        public Member Member { get; set; }
        public Reception Reception { get; set; }

    }
}
