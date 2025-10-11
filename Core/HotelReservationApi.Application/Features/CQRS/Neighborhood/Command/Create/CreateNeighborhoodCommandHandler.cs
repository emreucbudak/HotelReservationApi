using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Neighborhood.Command.Create
{
    public class CreateNeighborhoodCommandHandler : IRequestHandler<CreateNeighborhoodCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateNeighborhoodCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateNeighborhoodCommandRequest request, CancellationToken cancellationToken)
        {
            var neighborhood = new Domain.Entities.Neighborhood
            {
                Name = request.Name,
                DistrictId = request.DistrictId
            };
            await unitOfWork.writeRepository<Domain.Entities.Neighborhood>().AddAsync(neighborhood);
            await unitOfWork.SaveAsync();
        }
    }
}
