using HotelReservationApi.Application.Features.CQRS.Rooms.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using StackExchange.Redis;
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
        private readonly ConnectionMultiplexer connectionMultiplexer;
        public DeleteRoomsCommandHandler(IUnitOfWork unitOfWork, ConnectionMultiplexer connectionMultiplexer)
        {
            _unitOfWork = unitOfWork;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public async Task Handle(DeleteRoomsCommandRequest request, CancellationToken cancellationToken)
        {
            var room = await  _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Rooms>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            if (room is null)
            {
                throw new RoomsNotFoundExceptions(request.Id);
            }
            var cacheKey = $"rooms_{room.RoomTypes.HotelsId}_page_*";
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()[0]);
            var database = connectionMultiplexer.GetDatabase();
            await foreach (var key in server.KeysAsync(pattern: cacheKey, pageSize: 250))
            {
                await database.KeyDeleteAsync(key);
            }
            await _unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.Rooms>().DeleteAsync(room);
            await _unitOfWork.SaveAsync();

        }
    }
}
