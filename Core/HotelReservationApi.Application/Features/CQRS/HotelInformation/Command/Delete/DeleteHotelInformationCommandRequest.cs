using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelInformation.Command.Delete
{
    public class DeleteHotelInformationCommandRequest : IRequest
    {
        public int HotelInformationId { get; set; }

        public DeleteHotelInformationCommandRequest(int hotelInformationId)
        {
            HotelInformationId = hotelInformationId;
        }
    }
}
