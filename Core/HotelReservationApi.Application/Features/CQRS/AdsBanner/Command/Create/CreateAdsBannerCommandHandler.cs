using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.AdsBanner.Command.Create
{
    public class CreateAdsBannerCommandHandler : IRequestHandler<CreateAdsBannerCommandRequest>
    {
        private readonly IMapper mp;
        private readonly IUnitOfWork unitOfWork;

        public CreateAdsBannerCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {

            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateAdsBannerCommandRequest request, CancellationToken cancellationToken)
        {
            var adsBanner = mp.Map<HotelReservationApi.Domain.Entities.AdsBanner>(request);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.AdsBanner>().AddAsync(adsBanner);
            await unitOfWork.SaveAsync();
        }
    }
}
