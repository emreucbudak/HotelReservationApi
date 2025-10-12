using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Command.Update
{
    public class UpdateRoomsCommandHandler : IRequestHandler<UpdateRoomsCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateRoomsCommandRequest request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Rooms>().GetByExpression(enableTracking:false,predicate:x=> x.Id == request.RoomId);
            room.PriceListId = request.PriceListId;
            room.RoomNumber = request.RoomNumber;
            room.IsAvailable = request.IsAvailable;
            await _unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.Rooms>().UpdateAsync(room);
            await _unitOfWork.SaveAsync();
        }
    }
}
