using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelImages : BaseEntity
    {
        public HotelImages()
        {
        }

        public HotelImages(string ımageUrl, int hotelId, string? ımageTitle)
        {
            ImageUrl = ımageUrl;
            HotelId = hotelId;
            ImageTitle = ımageTitle;
        }

        public string ImageUrl { get; set; }
        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }
        public string? ImageTitle { get; set; }
    }
}
