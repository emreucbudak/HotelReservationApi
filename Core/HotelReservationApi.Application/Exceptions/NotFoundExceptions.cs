using HotelReservationApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Exceptions
{
    public class NotFoundExceptions : BasesException
    {
        public NotFoundExceptions(string message) : base(message)
        {
        }
    }
    
    }

