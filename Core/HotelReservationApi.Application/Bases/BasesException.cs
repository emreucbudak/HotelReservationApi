using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Bases
{
    public class BasesException : Exception
    {


        public BasesException(string message) : base(message)
        {
        }
    }
}
