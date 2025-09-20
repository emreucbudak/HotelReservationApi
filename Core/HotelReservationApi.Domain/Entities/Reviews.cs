using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Reviews : BaseEntity
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }  


    }
}
