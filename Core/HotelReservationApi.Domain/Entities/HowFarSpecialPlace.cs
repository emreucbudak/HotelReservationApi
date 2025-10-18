using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class HowFarSpecialPlace : BaseEntity
    {
        public HowFarSpecialPlace()
        {
        }

        public HowFarSpecialPlace(decimal distance, int hotelsId, string placeName, int specialPlaceCategoryId)
        {
            Distance = distance;
            HotelsId = hotelsId;
            PlaceName = placeName;
            SpecialPlaceCategoryId = specialPlaceCategoryId;
        }


        public string PlaceName { get; set; }
        public int SpecialPlaceCategoryId { get; set; }
        public SpecialPlaceCategory SpecialPlaceCategory { get; set; }
        public decimal Distance     { get; set; }
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }
    }
}
