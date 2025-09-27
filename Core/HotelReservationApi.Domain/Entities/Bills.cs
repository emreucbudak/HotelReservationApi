using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Bills : BaseEntity
    {
        public ICollection<Customer>? Customer { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


    }
}
