using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Create
{
    public class CreateHotelImagesCommandRequest : IRequest
    {
        public string ImageUrl { get; set; }
        public int HotelId { get; set; }
        public string? ImageTitle { get; set; }
    }
}
