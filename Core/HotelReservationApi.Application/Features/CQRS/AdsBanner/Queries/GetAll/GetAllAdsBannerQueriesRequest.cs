using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.AdsBanner.Queries.GetAll
{
    public class GetAllAdsBannerQueriesRequest : IRequest<GetAllAdsBannerQueriesResponse>
    {
    }
}
