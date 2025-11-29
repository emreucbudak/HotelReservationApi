using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.RabbitMq.Models
{
    public class BillPdfModel
    {
        public Guid BillNo { get; set; }
        public DateOnly BillCreateDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTiming { get; set; }
        public IList<string> PeopleBooked { get; set; }
        public int TotalAmount { get; set; }
        public int TotalNights { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string HotelName { get; set; }
        public IList<string> RoomTypes { get; set; }
    }
}
