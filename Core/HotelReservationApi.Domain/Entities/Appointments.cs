using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Appointments : BaseEntity
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int HowManyDays { get; set; } 
        public int RoomsId { get; set; }
        public Rooms Rooms { get; set; }
    }
}
