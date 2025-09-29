using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Service : BaseEntity
    {

        public string ServiceName { get; set; }
        public bool IsNeedAFee { get; set; }
    }
}
