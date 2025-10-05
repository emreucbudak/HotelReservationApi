using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Command.Create
{
    public class CreateFAQCommandRequest : IRequest
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int HotelID { get; set; }
    }
}
