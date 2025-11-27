using HotelReservationApi.Application.Bases;

namespace HotelReservationApi.Application.Exceptions
{
    public class AuthException : BasesException
    {


        public AuthException(string message) : base(message)
        {
        }
    }
}
