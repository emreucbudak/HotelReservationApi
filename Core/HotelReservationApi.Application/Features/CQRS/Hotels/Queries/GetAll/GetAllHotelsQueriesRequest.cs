using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Queries.GetAll
{
    public class GetAllHotelsQueriesRequest : IRequest<List<GetAllHotelsQueriesResponse>>
    {
        public GetAllHotelsQueriesRequest(int? pageNumber, int? pageSize)
        {
            this.pageNumber = pageNumber ?? 1;
            this.pageSize = pageSize ?? 10;
        }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
