using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Service.Command.Create
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public CreateServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateServiceCommandRequest request, CancellationToken cancellationToken)
        {
            var service = mp.Map<Domain.Entities.Service>(request); 
            await unitOfWork.writeRepository<Domain.Entities.Service>().AddAsync(service);
            await unitOfWork.SaveAsync();
        }
    }
}
