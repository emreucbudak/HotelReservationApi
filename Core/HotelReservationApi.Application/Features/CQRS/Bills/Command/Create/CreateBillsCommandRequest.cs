using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Command.Create
{
    public class CreateBillsCommandRequest : IRequest
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int PaymetMethodId { get; set; }
        public int PaymentTimingId { get; set; }
        public int ReservationId { get; set; }


    }
}
