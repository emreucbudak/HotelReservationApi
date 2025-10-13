using HotelReservationApi.Application.Features.CQRS.RoomTypes.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.RoomTypes.Command.Create
{
    public class CreateRoomTypesCommandHandler : IRequestHandler<CreateRoomTypesCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomTypesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateRoomTypesCommandRequest request, CancellationToken cancellationToken)
        {
           List<HotelReservationApi.Domain.Entities.TypesFeatures> typesFeatures = new();
            foreach (var featureId in request.TypesFeaturesIds)
            {
                var feature = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.TypesFeatures>().GetByExpression(predicate:x=> x.Id == featureId,enableTracking:false);                
                if (feature is null)
                {
                    throw new TypesFeaturesNotFoundExceptions(featureId);
                }
                typesFeatures.Add(feature);
                
                
            }
            Domain.Entities.RoomTypes roomTypes = new Domain.Entities.RoomTypes
            {
                TypeName = request.TypeName,
                HowManyPeople = request.HowManyPeople,
                TypesFeatures = typesFeatures
            };
            await _unitOfWork.writeRepository<Domain.Entities.RoomTypes>().AddAsync(roomTypes);
            await _unitOfWork.SaveAsync();

        }
    }
}
