using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Command.Create
{
    public class CreateHotelsCommandHandler : IRequestHandler<CreateHotelsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateHotelsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateHotelsCommandRequest request, CancellationToken cancellationToken)
        {
            var hotel = mapper.Map<Domain.Entities.Hotels>(request);
            await unitOfWork.writeRepository<Domain.Entities.Hotels>().AddAsync(hotel);
            await unitOfWork.SaveAsync();
        }
    }
}
