using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAll
{
    public class GetAllRoomsQueriesResponse
    {
        public int RoomNumber { get; set; }
        public bool IsAvailable { get; set; }
        public string TypeName { get; set; }
        public int HowManyPeople { get; set; }
        public List<string> FeatureName { get; set; }
        public int Price { get; set; }


    }
}
