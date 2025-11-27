using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class AdsBanner : BaseEntity
    {
        public AdsBanner()
        {
        }

        public AdsBanner(string title, string description, string ımageUrl)
        {
            Title = title;
            Description = description;
            ImageUrl = ımageUrl;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
