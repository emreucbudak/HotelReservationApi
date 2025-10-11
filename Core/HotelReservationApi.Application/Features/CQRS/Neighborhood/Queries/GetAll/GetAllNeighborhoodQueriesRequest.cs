using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Neighborhood.Queries.GetAll
{
    public class GetAllNeighborhoodQueriesRequest : IRequest<List<GetAllNeighborhoodQueriesResponse>>
    {
        public int DistrictId { get; set; }
    }
}
