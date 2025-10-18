using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByMemberId
{
    public class GetAllReservationByMemberIdQueriesRequest : IRequest<List<GetAllReservationByMemberIdQueriesResponse>>
    {
        public int MemberId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllReservationByMemberIdQueriesRequest(int memberId, int? pageNumber, int? pageSize)
        {
            MemberId = memberId;
            PageNumber = pageNumber ?? 1;
            PageSize = pageSize ?? 10;
        }
    }
}
