using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Queries.GetAll
{
    public class GetAllFAQQueriesResponse
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
