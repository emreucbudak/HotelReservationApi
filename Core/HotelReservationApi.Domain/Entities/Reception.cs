using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Reception : BaseEntity
    {
        public Reception()
        {
        }

        public Reception(Guid userId, int hotelsId)
        {
            UserId = userId;
            HotelsId = hotelsId;
        }

        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
