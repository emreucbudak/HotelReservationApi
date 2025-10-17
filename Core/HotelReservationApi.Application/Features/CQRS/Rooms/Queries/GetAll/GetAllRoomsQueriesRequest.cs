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
        public GetAllRoomsQueriesRequest(int id, int? pageCount, int? pageSize)
        {
            HotelId = id;
            Page = pageCount ?? 1;
            Size = pageSize ?? 10;
        }

        public int HotelId { get; set; }
        public int Page { get; set; } 
        public int Size { get; set; }

    }
}
