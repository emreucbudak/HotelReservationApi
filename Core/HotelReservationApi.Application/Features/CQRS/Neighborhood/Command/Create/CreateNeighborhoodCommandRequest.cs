using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Neighborhood.Command.Create
{
    public class CreateNeighborhoodCommandRequest : IRequest
    {
        public string Name { get; set; }
        public int DistrictId { get; set; }
    }
}
