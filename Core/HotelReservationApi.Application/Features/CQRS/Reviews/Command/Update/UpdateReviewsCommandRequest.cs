using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Command.Update
{
    public class UpdateReviewsCommandRequest : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        
    }
}
