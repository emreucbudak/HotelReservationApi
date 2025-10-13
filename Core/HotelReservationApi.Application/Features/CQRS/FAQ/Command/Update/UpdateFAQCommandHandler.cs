using HotelReservationApi.Application.Features.CQRS.FAQ.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Command.Update
{
    public class UpdateFAQCommandHandler : IRequestHandler<UpdateFAQCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFAQCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateFAQCommandRequest request, CancellationToken cancellationToken)
        {
            var faq = await _unitOfWork.readRepository<Domain.Entities.FAQ>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: true);
            if (faq is null)
            {
                throw new FAQNotFoundExceptions(request.Id);
            }
            faq.Question = request.Question;
            faq.Answer = request.Answer;
            await _unitOfWork.writeRepository<Domain.Entities.FAQ>().UpdateAsync(faq);
            await _unitOfWork.SaveAsync();
        }
    }
}
