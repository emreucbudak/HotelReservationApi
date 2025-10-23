using HotelReservationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAllForReservation
{
    public class GetAllRoomsForReservationQueriesResponse
    {
        public ICollection<Domain.Entities.TypesFeatures> typesFeatures {  get; set; }

        public string TypeName { get; set; }
        public int HowManyPeople { get; set; }
        public int DailyPrice { get; set; }
        public int? DiscountedPrice { get; set; }
        public int HotelId { get; set; }


    }
}
