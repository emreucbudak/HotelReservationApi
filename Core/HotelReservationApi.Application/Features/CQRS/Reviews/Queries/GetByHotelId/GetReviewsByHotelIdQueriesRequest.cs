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
    }
}
