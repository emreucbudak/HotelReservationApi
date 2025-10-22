using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Command.Create
{
    public class CreateRoomsCommandHandler : IRequestHandler<CreateRoomsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;
        private readonly ConnectionMultiplexer connectionMultiplexer;


        public CreateRoomsCommandHandler(IUnitOfWork unitOfWork, IMapper mp, ConnectionMultiplexer connectionMultiplexer)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public async Task Handle(CreateRoomsCommandRequest request, CancellationToken cancellationToken)
        {
            var newRoom = mp.Map<Domain.Entities.Rooms>(request);
            await unitOfWork.writeRepository<Domain.Entities.Rooms>().AddAsync(newRoom);
            await unitOfWork.SaveAsync();
            var cacheKey = $"rooms_{newRoom.HotelsId}_page_*";
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()[0]);
            var database = connectionMultiplexer.GetDatabase();
             await foreach (var key in server.KeysAsync(pattern:cacheKey,pageSize:250))
            {
                await database.KeyDeleteAsync(key);
            }
        }
    }
}
