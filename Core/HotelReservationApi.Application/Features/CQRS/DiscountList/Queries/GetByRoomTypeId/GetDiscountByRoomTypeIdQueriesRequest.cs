using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetByRoomTypeId
{
    public class GetDiscountByRoomTypeIdQueriesRequest : IRequest<List<GetDiscountByRoomTypeIdQueriesResponse>>
    {
        public int RoomTypeId { get; set; }

        public GetDiscountByRoomTypeIdQueriesRequest(int roomTypeId)
        {
            RoomTypeId = roomTypeId;
        }
    }
}
