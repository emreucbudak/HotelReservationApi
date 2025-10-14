using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.PriceList.Command.Create
{
    public class CreatePriceListCommandRequest : IRequest
    {
        public int Price { get; set; }
        public int DiscountListId { get; set; }
    }
}
