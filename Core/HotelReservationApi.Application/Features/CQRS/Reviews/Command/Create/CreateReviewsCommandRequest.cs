using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Command.Create
{
    public class CreateReviewsCommandRequest : IRequest
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public DateOnly ReviewDate { get; set; }
        public int HotelsId { get; set; }
    }
}
