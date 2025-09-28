using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Bills : BaseEntity
    {

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int PaymetMethodId { get; set; } 
        public PaymentMethod PaymetMethod { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int Price { get; set; }

    }
}
