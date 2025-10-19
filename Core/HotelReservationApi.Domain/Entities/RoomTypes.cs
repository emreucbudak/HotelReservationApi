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

        public RoomTypes(string typeName, int howManyPeople, ICollection<TypesFeatures> typesFeatures, int priceListId)
        {
            TypeName = typeName;
            HowManyPeople = howManyPeople;
            TypesFeatures = typesFeatures;
            PriceListId = priceListId;
        }

        public string TypeName { get; set; }
        public int HowManyPeople { get; set; }
        public ICollection<TypesFeatures> TypesFeatures { get; set; }
        public int PriceListId { get; set; }



        public PriceList PriceList { get; set; }

    }
}
