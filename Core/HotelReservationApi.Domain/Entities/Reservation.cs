using HotelReservationApi.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationApi.Domain.Entities
{
    [Index(nameof(StartDate), nameof(EndDate))]
    public class Reservation : BaseEntity
    {
        public Reservation()
        {
        }

        public Reservation(ICollection<Rooms> rooms, DateOnly startDate, DateOnly endDate, ICollection<Customer> customer, int hotelsId, int totalPrice, DateOnly reservationDate, int memberId)
        {
            Rooms = rooms;
            StartDate = startDate;
            EndDate = endDate;
            Customer = customer;
            HotelsId = hotelsId;
            TotalPrice = totalPrice;
            ReservationDate = reservationDate;
            MemberId = memberId;
        }

        public ICollection<Rooms> Rooms { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }  
        public int TotalPrice { get; set; } 
        public DateOnly ReservationDate { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public ICollection<ReservationRoom> reservationRooms { get; set; }



    }
}
