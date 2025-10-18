using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Create
{
    public class CreateHotelAdressCommandHandler : IRequestHandler<CreateHotelAdressCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public CreateHotelAdressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateHotelAdressCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelAdress = mapper.Map<Domain.Entities.HotelAdress>(request);
            await _unitOfWork.writeRepository<Domain.Entities.HotelAdress>().AddAsync(hotelAdress);
            await _unitOfWork.SaveAsync();
        }
    }
}
