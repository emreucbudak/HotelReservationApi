using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Exceptions
{
    public class AuthException : Exception
    {


        public AuthException(string message) : base(message)
        {
        }
    }
}
