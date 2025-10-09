using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Delete
{
    public class DeleteHotelCategoryCommandHandler : IRequestHandler<DeleteHotelCategoryCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHotelCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHotelCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelCategory = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.HotelCategory>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.HotelCategory>().DeleteAsync(hotelCategory);
            await unitOfWork.SaveAsync();
        }
    }
}
