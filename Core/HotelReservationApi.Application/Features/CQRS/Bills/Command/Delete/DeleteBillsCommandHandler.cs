using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Command.Delete
{
    public class DeleteBillsCommandHandler : IRequestHandler<DeleteBillsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteBillsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteBillsCommandRequest request, CancellationToken cancellationToken)
        {
            var bills = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Bills>().GetByExpression(predicate:x=> x.Id == request.Id,enableTracking:false);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.Bills>().DeleteAsync(bills);
            await unitOfWork.SaveAsync();
        }
    }
}
