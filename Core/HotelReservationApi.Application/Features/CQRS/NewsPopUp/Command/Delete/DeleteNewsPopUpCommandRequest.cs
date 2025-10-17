using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Delete
{
    public class DeleteNewsPopUpCommandRequest : IRequest
    {
        public DeleteNewsPopUpCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
