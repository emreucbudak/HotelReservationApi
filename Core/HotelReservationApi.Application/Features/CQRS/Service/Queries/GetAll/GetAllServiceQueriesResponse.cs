using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Service.Queries.GetAll
{
    public class GetAllServiceQueriesResponse
    {
        public string ServiceName { get; set; }
        public bool IsNeedAFee { get; set; }
    }
}
