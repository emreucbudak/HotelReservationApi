using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Hotels : IBaseEntity
    {
        public string HotelName { get; set; }
        public ICollection<Rooms> Rooms { get; set; }

    }
}
