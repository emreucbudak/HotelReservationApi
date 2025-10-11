using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Queries.GetAll
{
    public class GetAllHotelsServiceQueriesResponse
    {
        public string ServiceName { get; set; }
        public bool IsNeedAFee { get; set; }
    }
}
