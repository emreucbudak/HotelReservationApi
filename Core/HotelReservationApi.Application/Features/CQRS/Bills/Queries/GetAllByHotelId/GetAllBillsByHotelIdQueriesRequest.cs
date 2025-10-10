using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAllByHotelId
{
    public class GetAllBillsByHotelIdQueriesRequest : IRequest<List<GetAllBillsByHotelIdQueriesResponse>>
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int HotelId { get; set; }
    }
}
