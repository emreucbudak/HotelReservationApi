using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Create
{
    public class CreateHotelImagesCommandHandler : IRequestHandler<CreateHotelImagesCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public CreateHotelImagesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateHotelImagesCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelImages = mapper.Map<HotelReservationApi.Domain.Entities.HotelImages>(request);
            await _unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.HotelImages>().AddAsync(hotelImages);
            await _unitOfWork.SaveAsync();
                }
    }
}
