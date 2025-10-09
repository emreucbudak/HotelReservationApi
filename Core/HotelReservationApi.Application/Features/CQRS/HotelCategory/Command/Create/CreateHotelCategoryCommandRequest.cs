using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Create
{
    public class CreateHotelCategoryCommandRequest : IRequest
    {
        public string HotelCategoryName { get; set; }
    }
}
