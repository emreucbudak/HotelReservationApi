using HotelReservationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAllByHotelId
{
    public class GetAllBillsByHotelIdQueriesResponse
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Price { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public string MethodName { get; set; }
        public string TimingName { get; set; }
        public string HotelName { get; set; }
    }
}
