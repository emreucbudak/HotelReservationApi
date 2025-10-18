using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Delete
{
    public class DeleteHotelAdressCommandRequest : IRequest
    {
        public int HotelAdressId { get; set; }

        public DeleteHotelAdressCommandRequest(int hotelAdressId)
        {
            HotelAdressId = hotelAdressId;
        }
    }
}
