using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetByHotelsId
{
    public class GetDiscountListByHotelsIdQueriesRequest : IRequest<List<GetDiscountListByHotelsIdQueriesResponse>>
    {
        public int HotelsId { get; set; }

        public GetDiscountListByHotelsIdQueriesRequest(int hotelsId)
        {
            HotelsId = hotelsId;
        }
    }
}
