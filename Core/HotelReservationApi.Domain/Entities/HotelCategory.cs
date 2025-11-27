using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelCategory : BaseEntity
    {
        public HotelCategory()
        {
        }

        public HotelCategory(string hotelCategoryName)
        {
            HotelCategoryName = hotelCategoryName;
        }


        public string HotelCategoryName { get; set; }
    }
}
