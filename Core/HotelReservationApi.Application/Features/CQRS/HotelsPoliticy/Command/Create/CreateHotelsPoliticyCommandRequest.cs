using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Command.Create
{
    public class CreateHotelsPoliticyCommandRequest : IRequest
    {
        public string PoliticyName { get; set; }
        public string PoliticyDescription { get; set; }
        public int HotelId { get; set; }
    }
}
