using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelInformation.Command.Delete
{
    public class DeleteHotelInformationCommandHandler : IRequestHandler<DeleteHotelInformationCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHotelInformationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHotelInformationCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelInformation = await unitOfWork.readRepository<Domain.Entities.HotelInformation>().GetByExpression(enableTracking: false, predicate: x => x.Id == request.HotelInformationId);
            await unitOfWork.writeRepository<Domain.Entities.HotelInformation>().DeleteAsync(hotelInformation);
            await unitOfWork.SaveAsync();
        }
    }
}
