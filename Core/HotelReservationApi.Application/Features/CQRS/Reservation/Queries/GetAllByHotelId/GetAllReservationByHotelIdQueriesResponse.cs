using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByHotelId
{
    public class GetAllReservationByHotelIdQueriesResponse
    {
        public ICollection<Domain.Entities.Rooms> Rooms { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<Domain.Entities.Customer> Customer { get; set; }
        public int TotalPrice { get; set; }
    }
}
