using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Command.Create
{
    public class CreateHotelsServiceCommandRequest : IRequest
    {
        public int HotelsId { get; set; }
        public int ServiceId { get; set; }
    }
}
