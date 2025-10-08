using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Customer.Command.Delete
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = await unitOfWork.readRepository<Domain.Entities.Customer>().GetByExpression(predicate: x=> x.Id == request.Id,enableTracking:false);
            await unitOfWork.writeRepository<Domain.Entities.Customer>().DeleteAsync(customer);
            await unitOfWork.SaveAsync();
        }
    }
}
