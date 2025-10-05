using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetAll
{
    public class GetAllCouponQueriesRequest : IRequest<List<GetAllCouponQueriesResponse>>
    {
    }
}
