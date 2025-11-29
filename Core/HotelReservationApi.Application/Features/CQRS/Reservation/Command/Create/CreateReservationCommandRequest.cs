using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create
{
    public class CreateReservationCommandRequest : IRequest<CreateReservationCommandResponse>
    {
        public ICollection<Domain.Entities.Rooms> Rooms { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<Domain.Entities.Customer> Customer { get; set; }
        public int HotelsId { get; set; }
        public int MemberId { get; set; }

    }
}
