using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Command.Create
{
    public class CreateReviewsCommandHandler : IRequestHandler<CreateReviewsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;
        private readonly ConnectionMultiplexer connectionMultiplexer;

        public CreateReviewsCommandHandler(IUnitOfWork unitOfWork, IMapper mp, ConnectionMultiplexer connectionMultiplexer)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public async Task Handle(CreateReviewsCommandRequest request, CancellationToken cancellationToken)
        {
            var pattern = $"reviews_hotel_{request.HotelsId}_page_*";
            var review = mp.Map<Domain.Entities.Reviews>(request);
            await unitOfWork.writeRepository<Domain.Entities.Reviews>().AddAsync(review);
            await unitOfWork.SaveAsync();
            var database = connectionMultiplexer.GetDatabase();
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()[0]);
            await foreach (var keys in server.KeysAsync(pattern: pattern, pageSize: 250))
            {
               await  database.KeyDeleteAsync(keys);
            }
        }
    }
}
