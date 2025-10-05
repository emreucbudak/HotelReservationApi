using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Update
{
    public class UpdateNewsPopUpCommandHandler : IRequestHandler<UpdateNewsPopUpCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNewsPopUpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateNewsPopUpCommandRequest request, CancellationToken cancellationToken)
        {
            var news = await _unitOfWork.readRepository<Domain.Entities.NewsPopUp>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: true);
            news.Description = request.Description;
            news.ImageUrl = request.ImageUrl;
            news.Title = request.Title;
            await _unitOfWork.writeRepository<Domain.Entities.NewsPopUp>().UpdateAsync(news);
            await _unitOfWork.SaveAsync();
        }
    }
}
