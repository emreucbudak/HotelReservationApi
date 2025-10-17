using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Command.Delete
{
    public class DeleteBillsCommandRequest : IRequest
    {
        public int Id { get; set; }

        public DeleteBillsCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
