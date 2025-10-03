using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelAdress : BaseEntity
    {
        public int Id { get; set; }
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }  
        public int CityId { get; set; }
        public City City { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public string Street { get; set; }
        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        public double Enlem { get; set; }
        public double Boylam { get; set; }

        [NotMapped]
        public Point Location { get; set; }
    }
}
