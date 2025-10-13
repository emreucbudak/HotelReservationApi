using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.RoomTypes.Command.Delete
{
    public class DeleteRoomTypesCommandRequest : IRequest
    {
        public int Id { get; set; }
    }
}
