using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetByHotelId
{
    public class GetReviewsByHotelIdQueriesRequest : IRequest<List<GetReviewsByHotelIdQueriesResponse>>
    {

        public int HotelsId { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        public GetReviewsByHotelIdQueriesRequest(int hotelsId, int? number, int? size)
        {
            HotelsId = hotelsId;
            pageNumber = number ?? 1;
            pageSize = size ?? 10;
        }
    }
}
