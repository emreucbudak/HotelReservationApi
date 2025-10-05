using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Service.Queries.GetAll
{
    public class GetAllServiceQueriesRequest : IRequest<List<GetAllServiceQueriesResponse>>
    {
        public int Id    { get; set; }
    }
}
