using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class Member : BaseEntity
    {
        public Member()
        {
        }

        public Member(User userId, string referansCode, int coinCount)
        {
            this.User = userId;
            ReferansCode = referansCode;
            CoinCount = coinCount;
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public string ReferansCode { get; set; }
        public int CoinCount { get; set; }
    }
}
