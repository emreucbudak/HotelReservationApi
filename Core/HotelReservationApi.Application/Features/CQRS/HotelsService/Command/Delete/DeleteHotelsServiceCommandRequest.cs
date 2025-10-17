using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Command.Delete
{
    public class DeleteHotelsServiceCommandRequest : IRequest
    {
        public DeleteHotelsServiceCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
