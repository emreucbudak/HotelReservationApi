using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Command.Delete
{
    public class DeleteBillsCommandHandler : IRequestHandler<DeleteBillsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ConnectionMultiplexer redis;

        public DeleteBillsCommandHandler(IUnitOfWork unitOfWork, ConnectionMultiplexer redis)
        {
            this.unitOfWork = unitOfWork;
            this.redis = redis;
        }

        public async Task Handle(DeleteBillsCommandRequest request, CancellationToken cancellationToken)
        {
            var bills = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Bills>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.Bills>().DeleteAsync(bills);
            await unitOfWork.SaveAsync();
            var db = redis.GetDatabase();
            var server = redis.GetServer(redis.GetEndPoints()[0]);
            var pattern = $"bills_hotel_{bills.Reservation.HotelsId}_page_*";

            await foreach (var key in server.KeysAsync(pattern: pattern, pageSize: 1000))
            {
                await db.KeyDeleteAsync(key);
            }
        }
    }
}
