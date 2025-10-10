using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAll
{
    public class GetAllBillsQueriesRequest : IRequest<List<GetAllBillsQueriesResponse>>
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}
