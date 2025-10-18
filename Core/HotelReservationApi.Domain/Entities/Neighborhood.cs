using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Neighborhood : BaseEntity
    {
        public Neighborhood()
        {
        }

        public Neighborhood(string name, int districtId)
        {
            Name = name;
            DistrictId = districtId;
        }


        public string Name { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }

    }
}
