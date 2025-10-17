using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Service.Command.Delete
{
    public class DeleteServiceCommandRequest : IRequest
    {
        public DeleteServiceCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
