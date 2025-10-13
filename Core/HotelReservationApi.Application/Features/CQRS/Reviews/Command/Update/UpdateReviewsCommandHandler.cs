using HotelReservationApi.Application.Features.CQRS.Reviews.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Command.Update
{
    public class UpdateReviewsCommandHandler : IRequestHandler<UpdateReviewsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateReviewsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateReviewsCommandRequest request, CancellationToken cancellationToken)
        {
            var review = await unitOfWork.readRepository<Domain.Entities.Reviews>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:true);
            if (review is null)
            {
                throw new ReviewsNotFoundExceptions(request.Id);
            }
            review.Comment = request.Comment;
            review.Rating = request.Rating;
            review.Title = request.Title;
            review.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);
            await unitOfWork.writeRepository<Domain.Entities.Reviews>().UpdateAsync(review);
            await unitOfWork.SaveAsync();

        }
    }
}
