using HotelReservationApi.Application.Features.CQRS.Reviews.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
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

        public DeleteReviewsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteReviewsCommandRequest request, CancellationToken cancellationToken)
        {
            var review = await unitOfWork.readRepository<Domain.Entities.Reviews>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: false);
            if (review is null)
            {
                throw new ReviewsNotFoundExceptions(request.Id);
            }
            await unitOfWork.writeRepository<Domain.Entities.Reviews>().DeleteAsync(review);
            await unitOfWork.SaveAsync();
        }
    }
}
