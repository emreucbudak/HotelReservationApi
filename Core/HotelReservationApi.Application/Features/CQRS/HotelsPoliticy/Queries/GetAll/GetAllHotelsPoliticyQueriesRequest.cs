using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Queries.GetAll
{
    public class GetAllHotelsPoliticyQueriesRequest : IRequest<List<GetAllHotelsPoliticyQueriesResponse>>
    {
        public int HotelId { get; set; }    
    }
}
