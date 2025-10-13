using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.RoomTypes.Command.Delete
{
    public class DeleteRoomTypesCommandHandler : IRequestHandler<DeleteRoomTypesCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomTypesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRoomTypesCommandRequest request, CancellationToken cancellationToken)
        {
            var roomType = await _unitOfWork.readRepository<Domain.Entities.RoomTypes>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            await _unitOfWork.writeRepository<Domain.Entities.RoomTypes>().DeleteAsync(roomType);
            await _unitOfWork.SaveAsync();
        }
    }
}
