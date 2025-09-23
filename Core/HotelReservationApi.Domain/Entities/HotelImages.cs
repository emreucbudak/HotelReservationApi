using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelImages : BaseEntity
    {
        public string ImageUrl { get; set; }
        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }
        public string? ImageTitle { get; set; }
    }
}
