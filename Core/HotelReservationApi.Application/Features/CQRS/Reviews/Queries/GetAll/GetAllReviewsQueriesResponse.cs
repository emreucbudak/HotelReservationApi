using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetAll
{
    public class GetAllReviewsQueriesResponse 
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public DateOnly ReviewDate { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateOnly? UpdatedDate { get; set; }
        public string HotelName     { get; set; }
    }
}
