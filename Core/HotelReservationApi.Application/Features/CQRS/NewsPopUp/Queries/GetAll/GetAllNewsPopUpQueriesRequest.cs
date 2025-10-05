using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.NewsPopUp.Queries.GetAll
{
    public class GetAllNewsPopUpQueriesRequest : IRequest<List<GetAllNewsPopUpQueriesResponse>>
    {
    }
}
