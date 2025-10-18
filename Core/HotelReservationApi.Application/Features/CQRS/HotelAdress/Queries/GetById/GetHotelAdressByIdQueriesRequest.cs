using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Queries.GetById
{
    public class GetHotelAdressByIdQueriesRequest : IRequest<GetHotelAdressByIdQueriesResponse>
    {
        public int HotelId { get; set; }

        public GetHotelAdressByIdQueriesRequest(int hotelId)
        {
            HotelId = hotelId;
        }
    }
}
