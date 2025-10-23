using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.RoomTypes.Command.Create
{
    public class CreateRoomTypesCommandRequest : IRequest
    {
        public CreateRoomTypesCommandRequest(string typeName, int howManyPeople, List<int> typesFeaturesIds, int dailyPrice, int? discountedPrice)
        {
            TypeName = typeName;
            HowManyPeople = howManyPeople;
            TypesFeaturesIds = typesFeaturesIds;
            DailyPrice = dailyPrice;
            DiscountedPrice = discountedPrice;
        }

        public string TypeName { get; set; }
        public int HowManyPeople { get; set; }
        public List<int> TypesFeaturesIds { get; set; }
        public int DailyPrice { get; set; }
        public int? DiscountedPrice { get; set; }
    }
}
