using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
    }
}
