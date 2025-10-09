using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Command.Create
{
    public class CreateHowFarSpecialPlaceCommandRequest : IRequest
    {
        public string PlaceName { get; set; }
        public int SpecialPlaceCategoryId { get; set; }
        public decimal Distance { get; set; }
        public int HotelsId { get; set; }
    }
}
