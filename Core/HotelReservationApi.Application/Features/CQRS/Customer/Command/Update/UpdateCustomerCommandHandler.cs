using HotelReservationApi.Application.Features.CQRS.Customer.Exceptions;
using HotelReservationApi.Application.Features.CQRS.NewsPopUp.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Customer.Command.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = await unitOfWork.readRepository<Domain.Entities.Customer>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: true);
            if (customer is null)
            {
                throw new CustomerNotFoundExceptions(request.Id);
            }
            customer.Name = request.Name;
            customer.Surname = request.Surname;
            customer.BirthDate = request.BirthDate;
            customer.GenderId = request.GenderId;
            await unitOfWork.writeRepository<Domain.Entities.Customer>().UpdateAsync(customer);
            await unitOfWork.SaveAsync();
        }
    }
}
