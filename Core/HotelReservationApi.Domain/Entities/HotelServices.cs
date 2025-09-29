using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelServices : BaseEntity
    {
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }
       public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
