using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Queries.GetAllByHotelId
{
    public class GetAllHotelImagesByIdQueriesResponse
    {
        public string? ImageTitle { get; set; }
        public string ImageUrl { get; set; }
 
    }
}
