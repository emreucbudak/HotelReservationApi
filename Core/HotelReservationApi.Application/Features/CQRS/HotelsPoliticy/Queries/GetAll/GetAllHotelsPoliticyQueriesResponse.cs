using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Queries.GetAll
{
    public class GetAllHotelsPoliticyQueriesResponse
    {
        public string PoliticyName { get; set; }
        public string PoliticyDescription { get; set; }
    }
}
