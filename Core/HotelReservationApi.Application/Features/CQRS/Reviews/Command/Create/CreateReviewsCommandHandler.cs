using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
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

        public CreateReviewsCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateReviewsCommandRequest request, CancellationToken cancellationToken)
        {
            var review = mp.Map<Domain.Entities.Reviews>(request);
            await unitOfWork.writeRepository<Domain.Entities.Reviews>().AddAsync(review);
            await unitOfWork.SaveAsync();
        }
    }
}
