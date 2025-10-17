using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Delete
{
    public class DeleteReservationCommandRequest : IRequest
    {
        public DeleteReservationCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
