using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Admin : BaseEntity
    {
        public Admin(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; } 
        public User User { get; set; }
    }
}
