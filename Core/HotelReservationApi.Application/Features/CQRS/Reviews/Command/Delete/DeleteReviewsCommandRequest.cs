using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Command.Delete
{
    public class DeleteReviewsCommandRequest : IRequest
    {
        public int Id { get; set; }

        public DeleteReviewsCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
