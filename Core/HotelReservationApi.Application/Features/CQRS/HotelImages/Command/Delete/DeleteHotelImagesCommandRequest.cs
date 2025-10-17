using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Delete
{
    public class DeleteHotelImagesCommandRequest : IRequest
    {
        public int Id { get; set; }

        public DeleteHotelImagesCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
