using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create.CreateAfterBill
{
    public class CreateReservationAfterBillCommandHandler : IRequestHandler<CreateReservationAfterBillCommandRequest>
    {
        public Task Handle(CreateReservationAfterBillCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
