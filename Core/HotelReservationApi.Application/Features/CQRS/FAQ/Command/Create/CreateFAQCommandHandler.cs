using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Command.Create
{
    public class CreateFAQCommandHandler : IRequestHandler<CreateFAQCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public CreateFAQCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateFAQCommandRequest request, CancellationToken cancellationToken)
        {
            var mapped = mp.Map<HotelReservationApi.Domain.Entities.FAQ>(request);
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.FAQ>().AddAsync(mapped);
            await unitOfWork.SaveAsync();
        }
    }
}
