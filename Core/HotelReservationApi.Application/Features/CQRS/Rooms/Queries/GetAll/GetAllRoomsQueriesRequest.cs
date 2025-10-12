using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAll
{
    public class GetAllRoomsQueriesRequest : IRequest<List<GetAllRoomsQueriesResponse>>
    {
        public int HotelId { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
