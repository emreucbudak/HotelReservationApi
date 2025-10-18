using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelCategory : BaseEntity
    {
        public HotelCategory()
        {
        }

        public HotelCategory(string hotelCategoryName)
        {
            HotelCategoryName = hotelCategoryName;
        }


        public string HotelCategoryName { get; set; }
    }
}
