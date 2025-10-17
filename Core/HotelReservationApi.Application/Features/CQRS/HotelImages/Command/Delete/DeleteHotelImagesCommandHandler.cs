using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Delete
{
    public class DeleteHotelImagesCommandHandler : IRequestHandler<DeleteHotelImagesCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHotelImagesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHotelImagesCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelImages = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.HotelImages>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            await unitOfWork.writeRepository<Domain.Entities.HotelImages>().DeleteAsync(hotelImages);
            await unitOfWork.SaveAsync();
        }
    }
}
