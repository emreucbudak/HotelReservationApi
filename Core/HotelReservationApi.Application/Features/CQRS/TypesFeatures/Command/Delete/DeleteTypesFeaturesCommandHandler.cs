using HotelReservationApi.Application.Features.CQRS.TypesFeatures.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.TypesFeatures.Command.Delete
{
    public class DeleteTypesFeaturesCommandHandler : IRequestHandler<DeleteTypesFeaturesCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTypesFeaturesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteTypesFeaturesCommandRequest request, CancellationToken cancellationToken)
        {
            var typesFeature = await _unitOfWork.readRepository<Domain.Entities.TypesFeatures>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            if (typesFeature is null)
            {
                throw new TypesFeaturesNotFoundExceptions(request.Id);
            }
            await _unitOfWork.writeRepository<Domain.Entities.TypesFeatures>().DeleteAsync(typesFeature);
            await _unitOfWork.SaveAsync();

        }
    }
}
