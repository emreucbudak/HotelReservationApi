using HotelReservationApi.Application.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create.CreateAfterBill
{
    public class CreateReservationAfterBillCommandRequest : IRequest
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly ReservationDate { get; set; }
        public int MemberId { get; set; }
        public int HotelsId { get; set; }
        public ICollection<CustomerDTO> customerDto { get; set; }
        public ICollection<ReservationRoomDTO> ReservationRooms { get; set; }
    }
}
