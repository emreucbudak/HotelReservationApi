using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Queries.GetById
{
    public class GetHotelByIdQueriesResponse
    {
        public string HotelName { get; set; }
        public string HotelCategoryName { get; set; }
        public string Street { get; set; }
        public string CityName { get; set; }
        public int PostalCode { get; set; }
        public string DistrictName { get; set; }
        public string NeighboorhoodName { get; set; }
    }
}
