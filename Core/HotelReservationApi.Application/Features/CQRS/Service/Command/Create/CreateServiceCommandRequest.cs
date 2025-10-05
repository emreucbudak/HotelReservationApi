using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Service.Command.Create
{
    public class CreateServiceCommandRequest : IRequest
    {
        public string ServiceName { get; set; }
        public bool IsNeedAFee { get; set; }
        public int HotelsId { get; set; }
    }
}
