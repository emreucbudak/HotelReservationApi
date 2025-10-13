using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public ICollection<Rooms> Rooms { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }  
        public int TotalPrice { get; set; } 
        public DateOnly ReservationDate { get; set; }



    }
}
