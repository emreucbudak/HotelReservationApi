using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ConnectionMultiplexer connectionMultiplexer;

        public CreateReservationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ConnectionMultiplexer connectionMultiplexer)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public  async Task Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"reservation_hotel_{request.HotelsId}_page_*";
            var reservation = mapper.Map<Domain.Entities.Reservation>(request);
            await unitOfWork.writeRepository<Domain.Entities.Reservation>().AddAsync(reservation);
            await unitOfWork.SaveAsync();
            var database = connectionMultiplexer.GetDatabase();
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()[0]);
            await foreach (var keys in server.KeysAsync(pattern: cacheKey, pageSize: 250))
            {
                await database.KeyDeleteAsync(keys);
            }
        }
    }
}
