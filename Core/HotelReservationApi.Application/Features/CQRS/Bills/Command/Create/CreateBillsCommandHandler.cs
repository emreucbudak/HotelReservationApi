using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Command.Create
{
    public class CreateBillsCommandHandler : IRequestHandler<CreateBillsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;
        private readonly ConnectionMultiplexer redis;

        public CreateBillsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache, ConnectionMultiplexer redis)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cache = cache;
            this.redis = redis;
        }

        public async Task Handle(CreateBillsCommandRequest request, CancellationToken cancellationToken)
        {
            var newBill = mapper.Map<Domain.Entities.Bills>(request);   
            await unitOfWork.writeRepository<Domain.Entities.Bills>().AddAsync(newBill);
            await unitOfWork.SaveAsync();
            var db = redis.GetDatabase();
            var server = redis.GetServer(redis.GetEndPoints()[0]);
            var pattern = $"bills_hotel_{newBill.Reservation.HotelsId}_page_*";

           await foreach (var key in server.KeysAsync(pattern: pattern,pageSize:1000))
            {
                await db.KeyDeleteAsync(key);
            }

        }
    }
}
