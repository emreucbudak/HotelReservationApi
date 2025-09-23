using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class PriceList : BaseEntity
    {
        public int Price { get; set; }
        public bool IsDiscount { get; set; } = false;
        public int? DiscountPercentage { get; set; }
        public int? DiscountPrice { get; set; }

    }
}
