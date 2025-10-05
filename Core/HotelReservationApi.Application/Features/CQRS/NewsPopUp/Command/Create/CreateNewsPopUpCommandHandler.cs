using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Create
{
    public class CreateNewsPopUpCommandHandler : IRequestHandler<CreateNewsPopUpCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mp;

        public CreateNewsPopUpCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            _unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateNewsPopUpCommandRequest request, CancellationToken cancellationToken)
        {
            var news = mp.Map<Domain.Entities.NewsPopUp>(request);
            await _unitOfWork.writeRepository<Domain.Entities.NewsPopUp>().AddAsync(news);
            await _unitOfWork.SaveAsync();
        }
    }
}
