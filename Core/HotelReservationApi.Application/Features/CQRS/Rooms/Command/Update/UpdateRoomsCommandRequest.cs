using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Command.Update
{
    public class UpdateRoomsCommandRequest : IRequest
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public bool IsAvailable { get; set; }
        public int PriceListId { get; set; }
    }
}
