using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int HotelID  { get; set; }
        public Hotels Hotel { get; set; }   
    }
}
