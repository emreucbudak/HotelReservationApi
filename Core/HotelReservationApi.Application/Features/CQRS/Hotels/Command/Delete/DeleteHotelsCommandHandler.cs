using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Command.Delete
{
    public class DeleteHotelsCommandHandler : IRequestHandler<DeleteHotelsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHotelsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHotelsCommandRequest request, CancellationToken cancellationToken)
        {
            var hotels = await unitOfWork.readRepository<Domain.Entities.Hotels>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            await unitOfWork.writeRepository<Domain.Entities.Hotels>().DeleteAsync(hotels);
            await unitOfWork.SaveAsync();
        }
    }
}
