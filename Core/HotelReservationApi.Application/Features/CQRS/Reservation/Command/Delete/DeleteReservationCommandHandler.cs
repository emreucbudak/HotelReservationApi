using HotelReservationApi.Application.Features.CQRS.Reservation.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Delete
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteReservationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public  async Task Handle(DeleteReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var reservation = await unitOfWork.readRepository<Domain.Entities.Reservation>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: true);
            if (reservation is null)
            {
                throw new ReservationNotFoundExceptions(request.Id);
            }
            await unitOfWork.writeRepository<Domain.Entities.Reservation>().DeleteAsync(reservation);
            await unitOfWork.SaveAsync();
        }
    }
}
