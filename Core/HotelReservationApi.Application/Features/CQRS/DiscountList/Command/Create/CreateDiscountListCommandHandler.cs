using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Command.Create
{
    public class CreateDiscountListCommandHandler : IRequestHandler<CreateDiscountListCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateDiscountListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateDiscountListCommandRequest request, CancellationToken cancellationToken)
        {
            var newDiscountList = mapper.Map<Domain.Entities.DiscountList>(request);
            await unitOfWork.writeRepository<Domain.Entities.DiscountList>().AddAsync(newDiscountList);
            await unitOfWork.SaveAsync();
        }
    }
}
