using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Delete
{
    public class DeleteNewsPopUpCommandHandler : IRequestHandler<DeleteNewsPopUpCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteNewsPopUpCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteNewsPopUpCommandRequest request, CancellationToken cancellationToken)
        {
            var news = await unitOfWork.readRepository<Domain.Entities.NewsPopUp>().GetByExpression(predicate: x=> x.Id == request.Id,enableTracking:false);
            await  unitOfWork.writeRepository<Domain.Entities.NewsPopUp>().DeleteAsync(news);
            await unitOfWork.SaveAsync();

        }
    }
}
