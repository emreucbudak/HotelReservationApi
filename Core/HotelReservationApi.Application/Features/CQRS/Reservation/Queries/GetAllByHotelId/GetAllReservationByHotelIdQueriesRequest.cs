using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByHotelId
{
    public class GetAllReservationByHotelIdQueriesRequest : IRequest<List<GetAllReservationByHotelIdQueriesResponse>>
    {
        private int id;

        public GetAllReservationByHotelIdQueriesRequest(int id)
        {
            this.id = id;
        }

        public int HotelsId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
