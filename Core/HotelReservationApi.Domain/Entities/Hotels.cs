using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Hotels : BaseEntity
    {
        public string HotelName { get; set; }
        public ICollection<Rooms> Rooms { get; set; }
        public int HotelCategoryId { get; set; }    
        public HotelCategory HotelCategory { get; set; }


    }
}
