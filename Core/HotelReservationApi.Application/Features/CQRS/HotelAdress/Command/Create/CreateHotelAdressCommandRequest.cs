using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Create
{
    public class CreateHotelAdressCommandRequest : IRequest
    {


        public int HotelsId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Street { get; set; }
        public int NeighborhoodId { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }
    }
}
