using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Queries.GetById
{
    public class GetHotelByIdQueriesRequest : IRequest<GetHotelByIdQueriesResponse>
    {
        public int HotelsId { get; set; }

        public GetHotelByIdQueriesRequest(int hotelsId)
        {
            HotelsId = hotelsId;
        }
    }
}
