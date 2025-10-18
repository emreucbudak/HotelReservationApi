using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Queries.GetById
{
    public class GetHotelAdressByIdQueriesResponse
    {
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public string Street { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string NeighborhoodName { get; set; }
    }
}
