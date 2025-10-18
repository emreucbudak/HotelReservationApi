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
        public PriceList()
        {
        }

        public PriceList(int price, int discountListId)
        {
            Price = price;
            DiscountListId = discountListId;
        }


        public int Price { get; set; }
        public int DiscountListId { get; set; }
        public DiscountList? DiscountList { get; set; }


    }
}
