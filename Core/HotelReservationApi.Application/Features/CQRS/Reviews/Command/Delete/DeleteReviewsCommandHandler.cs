using HotelReservationApi.Application.Features.CQRS.Reviews.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Command.Delete
{
    public class DeleteReviewsCommandHandler : IRequestHandler<DeleteReviewsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ConnectionMultiplexer connectionMultiplexer;

        public DeleteReviewsCommandHandler(IUnitOfWork unitOfWork, ConnectionMultiplexer connectionMultiplexer)
        {
            this.unitOfWork = unitOfWork;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public async Task Handle(DeleteReviewsCommandRequest request, CancellationToken cancellationToken)
        {
            
            var review = await unitOfWork.readRepository<Domain.Entities.Reviews>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: false);
            
            if (review is null)
            {
                throw new ReviewsNotFoundExceptions(request.Id);
            }
            var pattern = $"reviews_hotel_{review.HotelsId}_page_*";
            var database = connectionMultiplexer.GetDatabase();
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()[0]);
            await foreach (var keys in server.KeysAsync(pattern: pattern, pageSize: 250))
            {
                await database.KeyDeleteAsync(keys);
            }
            await unitOfWork.writeRepository<Domain.Entities.Reviews>().DeleteAsync(review);
            await unitOfWork.SaveAsync();
        }
    }
}
