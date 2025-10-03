using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.Coupon.Command.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<CreateCouponCommandRequest, Domain.Entities.Coupon>().ReverseMap();
        }
    }
}
