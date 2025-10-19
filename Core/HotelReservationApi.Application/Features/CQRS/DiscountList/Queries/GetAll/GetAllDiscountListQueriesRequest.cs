using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetAll
{
    public class GetAllDiscountListQueriesRequest : IRequest<List<GetAllDiscountListQueriesResponse>>
    {
    }
}
