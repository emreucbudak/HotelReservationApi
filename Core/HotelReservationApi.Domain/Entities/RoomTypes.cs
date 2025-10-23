using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class RoomTypes  : BaseEntity
    {
        public RoomTypes()
        {
        }

        public RoomTypes(string typeName, int howManyPeople, ICollection<TypesFeatures> typesFeatures, int dailyPrice, int? discountedPrice, int hotelsId)
        {
            TypeName = typeName;
            HowManyPeople = howManyPeople;
            TypesFeatures = typesFeatures;
            DailyPrice = dailyPrice;
            DiscountedPrice = discountedPrice;
            HotelsId = hotelsId;
        }

        public string TypeName { get; set; }
        public int HowManyPeople { get; set; }
        public ICollection<TypesFeatures> TypesFeatures { get; set; }
        public int DailyPrice {  get; set; }
        public int? DiscountedPrice { get; set; }

        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }

    }
}
