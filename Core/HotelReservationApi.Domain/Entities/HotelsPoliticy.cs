using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelsPoliticy : BaseEntity
    {
        public HotelsPoliticy()
        {
        }

        public HotelsPoliticy(string politicyName, string politicyDescription, int hotelId)
        {
            PoliticyName = politicyName;
            PoliticyDescription = politicyDescription;
            HotelId = hotelId;
        }


        public string PoliticyName { get; set; }
        public string PoliticyDescription { get; set; }
        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }
    }
}
