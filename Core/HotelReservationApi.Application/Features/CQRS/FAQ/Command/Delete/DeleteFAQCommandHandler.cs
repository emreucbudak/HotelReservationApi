using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Command.Delete
{
    public class DeleteFAQCommandHandler : IRequestHandler<DeleteFAQCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFAQCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteFAQCommandRequest request, CancellationToken cancellationToken)
        {
            var faq = await _unitOfWork.readRepository<Domain.Entities.FAQ>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: false);
            await _unitOfWork.writeRepository<Domain.Entities.FAQ>().DeleteAsync(faq);
            await _unitOfWork.SaveAsync();
        }
    }
}
