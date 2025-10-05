using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.NewsPopUp.Queries.GetAll
{
    public class GetAllNewsPopUpQueriesHandler : IRequestHandler<GetAllNewsPopUpQueriesRequest, List<GetAllNewsPopUpQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllNewsPopUpQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetAllNewsPopUpQueriesResponse>> Handle(GetAllNewsPopUpQueriesRequest request, CancellationToken cancellationToken)
        {
            var news = await unitOfWork.readRepository<Domain.Entities.NewsPopUp>().GetAllAsync(enableTracking: false);
            var popup = mapper.Map<List<GetAllNewsPopUpQueriesResponse>>(news);
            return popup;
        }
    }
}
