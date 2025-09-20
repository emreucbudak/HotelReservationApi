using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class RoomTypes  
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int HowManyPeople { get; set; }
        public int TypesFeaturesId { get; set; }
        public TypesFeatures TypesFeatures { get; set; }

    }
}
