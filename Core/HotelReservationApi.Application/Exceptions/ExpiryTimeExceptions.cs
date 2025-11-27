using HotelReservationApi.Application.Bases;

namespace HotelReservationApi.Application.Exceptions
{
    public class ExpiryTimeExceptions : BasesException
    {

        public ExpiryTimeExceptions(string message) : base(message)
        {
        }
    }
}
