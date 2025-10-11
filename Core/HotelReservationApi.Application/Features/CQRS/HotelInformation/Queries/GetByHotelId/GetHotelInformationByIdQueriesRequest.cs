using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelInformation.Queries.GetByHotelId
{
    public class GetHotelInformationByIdQueriesRequest : IRequest<GetHotelInformationByIdQueriesResponse>   
    {

        public int HotelsId  { get; set; }
    }
}
