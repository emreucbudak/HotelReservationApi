using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Neighborhood.Command.Delete
{
    public class DeleteNeighborhoodCommandHandler : IRequestHandler<DeleteNeighborhoodCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteNeighborhoodCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteNeighborhoodCommandRequest request, CancellationToken cancellationToken)
        {
            var neighborhood = await unitOfWork.readRepository<Domain.Entities.Neighborhood>().GetByExpression(enableTracking: false, predicate: x => x.Id == request.Id);
            await unitOfWork.writeRepository<Domain.Entities.Neighborhood>().DeleteAsync(neighborhood);
            await unitOfWork.SaveAsync();
        }
    }
}
