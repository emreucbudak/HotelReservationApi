using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelAdress : BaseEntity
    {
        public HotelAdress()
        {
        }

        public HotelAdress(int hotelsId, int cityId, int districtId, string street, int neighborhoodId, double enlem, double boylam)
        {
            HotelsId = hotelsId;
            CityId = cityId;
            DistrictId = districtId;
            Street = street;
            NeighborhoodId = neighborhoodId;
            Enlem = enlem;
            Boylam = boylam;
        }

        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }  
        public int CityId { get; set; }
        public City City { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }

        public string Street { get; set; }
        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        public double Enlem { get; set; }
        public double Boylam { get; set; }


    }
}
