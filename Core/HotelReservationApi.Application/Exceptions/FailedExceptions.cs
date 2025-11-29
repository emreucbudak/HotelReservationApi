using HotelReservationApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Exceptions
{
    public class FailedExceptions : BasesException
    {
        public FailedExceptions(string islem) : base($"{islem} basarisiz oldu!")
        {
        }
    }
}
