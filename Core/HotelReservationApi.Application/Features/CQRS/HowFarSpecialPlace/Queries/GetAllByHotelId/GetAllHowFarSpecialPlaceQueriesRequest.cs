using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Queries.GetAllByHotelId
{
    public class GetAllHowFarSpecialPlaceQueriesRequest : IRequest <List<GetAllHowFarSpecialPlaceQueriesResponse>>
    {
        public int HotelId { get; set; }
        }
}
