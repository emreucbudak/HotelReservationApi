using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Exceptions
{
    public class UsageLimitExceptions : Exception
    {
        
        public UsageLimitExceptions(string? message) : base(message)
        {
        }
    }
}
