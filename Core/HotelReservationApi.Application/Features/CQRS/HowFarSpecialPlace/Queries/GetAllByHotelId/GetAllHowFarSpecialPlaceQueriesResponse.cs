using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Queries.GetAllByHotelId
{
    public class GetAllHowFarSpecialPlaceQueriesResponse
    {
        public string PlaceName { get; set; }
        public decimal Distance { get; set; }
        public string SpecialPlaceCategoryName { get; set; }
    }
}
