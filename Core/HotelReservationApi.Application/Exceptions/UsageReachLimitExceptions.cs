using HotelReservationApi.Application.Bases;

namespace HotelReservationApi.Application.Exceptions
{
    public class UsageReachLimitExceptions : BasesException
    {
    
        public UsageReachLimitExceptions(string message) : base(message)
        {
        }
    }
}
