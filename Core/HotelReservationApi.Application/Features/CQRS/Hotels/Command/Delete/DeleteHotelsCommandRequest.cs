using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Command.Delete
{
    public class DeleteHotelsCommandRequest : IRequest
    {
        public DeleteHotelsCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
