using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Command.Create
{
    public class CreateHotelsPoliticyCommandHandler : IRequestHandler<CreateHotelsPoliticyCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateHotelsPoliticyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateHotelsPoliticyCommandRequest request, CancellationToken cancellationToken)
        {
            var newHotelsPoliticy = mapper.Map<Domain.Entities.HotelsPoliticy>(request);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.HotelsPoliticy>().AddAsync(newHotelsPoliticy);
            await unitOfWork.SaveAsync();
        }
    }
}
