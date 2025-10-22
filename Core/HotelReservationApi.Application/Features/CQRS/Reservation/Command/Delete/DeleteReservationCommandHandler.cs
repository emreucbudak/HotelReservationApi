using HotelReservationApi.Application.Features.CQRS.Reservation.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Delete
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ConnectionMultiplexer connectionMultiplexer;

        public DeleteReservationCommandHandler(IUnitOfWork unitOfWork, ConnectionMultiplexer connectionMultiplexer)
        {
            this.unitOfWork = unitOfWork;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public  async Task Handle(DeleteReservationCommandRequest request, CancellationToken cancellationToken)
        {
  
            var reservation = await unitOfWork.readRepository<Domain.Entities.Reservation>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: true);

            if (reservation is null)
            {
                throw new ReservationNotFoundExceptions(request.Id);
            }
            var cacheKey = $"reservation_hotel_{reservation.HotelsId}_page_*";
            await unitOfWork.writeRepository<Domain.Entities.Reservation>().DeleteAsync(reservation);
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
