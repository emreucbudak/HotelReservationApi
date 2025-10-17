using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Command.Update
{
    public class UpdateHotelsCommandRequest : IRequest
    {
        public int HotelsId { get; set; }
        public string HotelsName { get; set; }
    }
}
