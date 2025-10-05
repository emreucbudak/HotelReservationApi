using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Service.Command.Delete
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteServiceCommandRequest request, CancellationToken cancellationToken)
        {
            var service =  await unitOfWork.readRepository<Domain.Entities.Service>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: false);
            await unitOfWork.writeRepository<Domain.Entities.Service>().DeleteAsync(service);
            await unitOfWork.SaveAsync();
        }
    }
}
