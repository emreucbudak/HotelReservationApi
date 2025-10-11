using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelInformation.Command.Create
{
    public class CreateHotelInformationCommandHandler : IRequestHandler<CreateHotelInformationCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public CreateHotelInformationCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateHotelInformationCommandRequest request, CancellationToken cancellationToken)
        {
            var newHotelInformation = mp.Map<Domain.Entities.HotelInformation>(request);
            await unitOfWork.writeRepository<Domain.Entities.HotelInformation>().AddAsync(newHotelInformation);
            await unitOfWork.SaveAsync();
        }
    }
}
