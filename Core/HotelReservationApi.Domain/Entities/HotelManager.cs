using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelManager : BaseEntity
    {
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
