using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Queries.GetAll
{
    public class GetAllHotelsServiceQueriesRequest : IRequest<List<GetAllHotelsServiceQueriesResponse>>
    {
        public GetAllHotelsServiceQueriesRequest(int hotelsId)
        {
            HotelsId = hotelsId;
        }

        public int HotelsId { get; set; }
    }
}
