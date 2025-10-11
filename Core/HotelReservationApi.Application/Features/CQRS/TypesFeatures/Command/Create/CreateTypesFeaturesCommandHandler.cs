using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.TypesFeatures.Command.Create
{
    public class CreateTypesFeaturesCommandHandler : IRequestHandler<CreateTypesFeaturesCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTypesFeaturesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateTypesFeaturesCommandRequest request, CancellationToken cancellationToken)
        {
            var typesFeatures = new HotelReservationApi.Domain.Entities.TypesFeatures()
            {
                FeatureName = request.FeatureName
            };
            await _unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.TypesFeatures>().AddAsync(typesFeatures);
            await  _unitOfWork.SaveAsync();
        }
    }
}
