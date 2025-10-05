using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Queries.GetAll
{
    public class GetAllFAQQueriesRequest : IRequest<List<GetAllFAQQueriesResponse>>
    {
        public int HotelID { get; set; } 
        }
}
