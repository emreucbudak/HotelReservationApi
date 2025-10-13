using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.RoomTypes.Command.Create
{
    public class CreateRoomTypesCommandRequest : IRequest
    {
        public string TypeName { get; set; }
        public int HowManyPeople { get; set; }
        public List<int> TypesFeaturesIds { get; set; }
    }
}
