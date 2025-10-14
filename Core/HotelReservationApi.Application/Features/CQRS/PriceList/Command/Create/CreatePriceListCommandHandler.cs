using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.PriceList.Command.Create
{
    public class CreatePriceListCommandHandler : IRequestHandler<CreatePriceListCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public CreatePriceListCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreatePriceListCommandRequest request, CancellationToken cancellationToken)
        {
            var priceList = mp.Map<HotelReservationApi.Domain.Entities.PriceList>(request);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.PriceList>().AddAsync(priceList);
            await  unitOfWork.SaveAsync();
        }
    }
}
