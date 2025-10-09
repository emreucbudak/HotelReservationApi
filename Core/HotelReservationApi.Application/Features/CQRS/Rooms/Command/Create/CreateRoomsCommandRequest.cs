using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Command.Create
{
    public class CreateRoomsCommandRequest : IRequest
    {
        public int RoomNumber { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int PriceListId { get; set; }
        public int RoomTypesId { get; set; }
        public int HotelsId { get; set; }
    }
}
