using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Command.Delete
{
    public class DeleteRoomsCommandHandler : IRequestHandler<DeleteRoomsCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRoomsCommandRequest request, CancellationToken cancellationToken)
        {
            var room = await  _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Rooms>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            await _unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.Rooms>().DeleteAsync(room);
            await _unitOfWork.SaveAsync();
        }
    }
}
