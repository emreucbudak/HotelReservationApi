using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Command.Delete
{
    public class DeleteDiscountListCommandRequest : IRequest
    {
        public int DiscountListId { get; set; }

        public DeleteDiscountListCommandRequest(int discountListId)
        {
            DiscountListId = discountListId;
        }
    }
}
