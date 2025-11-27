using HotelReservationApi.Application.Bases;

namespace HotelReservationApi.Application.Exceptions
{
    public class NotFoundExceptions : BasesException
    {
        public NotFoundExceptions(string message) : base(message)
        {
        }
    }
    
    }

