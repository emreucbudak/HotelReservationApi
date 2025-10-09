using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Delete
{
    public class DeleteHotelCategoryCommandRequest : IRequest
    {
        public int Id { get; set; }
    }
}
