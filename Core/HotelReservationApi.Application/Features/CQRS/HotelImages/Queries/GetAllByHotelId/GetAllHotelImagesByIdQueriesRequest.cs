using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Queries.GetAllByHotelId
{
    public class GetAllHotelImagesByIdQueriesRequest : IRequest<List<GetAllHotelImagesByIdQueriesResponse>>
    {
        public int HotelId { get; set; }

        public GetAllHotelImagesByIdQueriesRequest(int hotelId)
        {
            HotelId = hotelId;
        }
    }
}
