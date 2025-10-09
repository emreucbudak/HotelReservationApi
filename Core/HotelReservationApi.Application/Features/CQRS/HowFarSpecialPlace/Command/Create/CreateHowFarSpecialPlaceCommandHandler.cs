using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Command.Create
{
    public class CreateHowFarSpecialPlaceCommandHandler : IRequestHandler<CreateHowFarSpecialPlaceCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateHowFarSpecialPlaceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateHowFarSpecialPlaceCommandRequest request, CancellationToken cancellationToken)
        {
            var newHowFarSpecialPlace = mapper.Map<Domain.Entities.HowFarSpecialPlace>(request);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.HowFarSpecialPlace>().AddAsync(newHowFarSpecialPlace);
            await unitOfWork.SaveAsync();
        }
    }
}
