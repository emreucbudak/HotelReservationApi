using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Customer.Command.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = mp.Map<Domain.Entities.Customer>(request);
            await unitOfWork.writeRepository<Domain.Entities.Customer>().AddAsync(customer);
            await unitOfWork.SaveAsync();
        }
    }
}
