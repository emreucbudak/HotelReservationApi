using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Command.Delete
{
    public class DeleteFAQCommandRequest : IRequest
    {
        public int Id   { get; set; }

        public DeleteFAQCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
