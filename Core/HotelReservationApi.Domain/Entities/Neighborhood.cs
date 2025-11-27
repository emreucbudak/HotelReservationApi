using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class Neighborhood : BaseEntity
    {
        public Neighborhood()
        {
        }

        public Neighborhood(string name, int districtId)
        {
            Name = name;
            DistrictId = districtId;
        }


        public string Name { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }

    }
}
