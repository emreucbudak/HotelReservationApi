using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Command.Delete
{
    public class DeleteDiscountListCommandHandler : IRequestHandler<DeleteDiscountListCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteDiscountListCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDiscountListCommandRequest request, CancellationToken cancellationToken)
        {
            var discountList = await unitOfWork.readRepository<Domain.Entities.DiscountList>().GetByExpression(enableTracking:false,predicate:x=> x.Id == request.DiscountListId);
            await unitOfWork.writeRepository<Domain.Entities.DiscountList>().DeleteAsync(discountList);
            await unitOfWork.SaveAsync();
        }
    }
}
