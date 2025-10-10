using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Command.Create
{
    public class CreateBillsCommandHandler : IRequestHandler<CreateBillsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateBillsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateBillsCommandRequest request, CancellationToken cancellationToken)
        {
            var newBill = mapper.Map<Domain.Entities.Bills>(request);   
            await unitOfWork.writeRepository<Domain.Entities.Bills>().AddAsync(newBill);
            await unitOfWork.SaveAsync();
        }
    }
}
