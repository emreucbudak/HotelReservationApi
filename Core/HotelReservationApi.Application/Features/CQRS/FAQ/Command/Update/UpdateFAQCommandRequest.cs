using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Command.Update
{
    public class UpdateFAQCommandRequest : IRequest
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}
