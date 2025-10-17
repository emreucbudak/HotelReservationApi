using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Command.Create
{
    public class CreateHotelsCommandRequest : IRequest
    {
        public string HotelName { get; set; }
        public int HotelCategoryId { get; set; }
        public int HotelAdressId { get; set; }
    }
}
