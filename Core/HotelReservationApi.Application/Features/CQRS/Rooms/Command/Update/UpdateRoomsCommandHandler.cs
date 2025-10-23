using HotelReservationApi.Application.Features.CQRS.Rooms.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using StackExchange.Redis;
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
        private readonly ConnectionMultiplexer connectionMultiplexer;

        public UpdateRoomsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateRoomsCommandRequest request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Rooms>().GetByExpression(enableTracking:false,predicate:x=> x.Id == request.RoomId);
            if (room is null)
            {
                throw new RoomsNotFoundExceptions(request.RoomId);
            }
            room.RoomNumber = request.RoomNumber;
            room.IsAvailable = request.IsAvailable;
            await _unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.Rooms>().UpdateAsync(room);
            await _unitOfWork.SaveAsync();
            var cacheKey = $"rooms_{room.RoomTypes.HotelsId}_page_*";
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()[0]);
            var database = connectionMultiplexer.GetDatabase();
            await foreach (var key in server.KeysAsync(pattern: cacheKey, pageSize: 250))
            {
                await database.KeyDeleteAsync(key);
            }
        }
    }
}
