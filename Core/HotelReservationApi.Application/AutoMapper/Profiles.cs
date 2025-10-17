using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.AdsBanner.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Bills.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAllByHotelId;
using HotelReservationApi.Application.Features.CQRS.Coupon.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Customer.Command.Create;
using HotelReservationApi.Application.Features.CQRS.FAQ.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelImages.Queries.GetAllByHotelId;
using HotelReservationApi.Application.Features.CQRS.HotelInformation.Queries.GetByHotelId;
using HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Neighborhood.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByHotelId;
using HotelReservationApi.Application.Features.CQRS.Reviews.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Service.Command.Create;
using HotelReservationApi.Domain.Entities;
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
            CreateMap<CreateAdsBannerCommandRequest, Domain.Entities.AdsBanner>().ReverseMap();
            CreateMap<Domain.Entities.AdsBanner, Features.CQRS.AdsBanner.Queries.GetAll.GetAllAdsBannerQueriesResponse>().ReverseMap();
            CreateMap<Domain.Entities.Coupon, Features.CQRS.Coupon.Queries.GetAll.GetAllCouponQueriesResponse>().ReverseMap();
            CreateMap<CreateNewsPopUpCommandRequest, Domain.Entities.NewsPopUp>().ReverseMap();
            CreateMap<NewsPopUp, Features.CQRS.NewsPopUp.Queries.GetAll.GetAllNewsPopUpQueriesResponse>().ReverseMap();
            CreateMap<CreateFAQCommandRequest, Domain.Entities.FAQ>().ReverseMap();
            CreateMap<FAQ, Features.CQRS.FAQ.Queries.GetAll.GetAllFAQQueriesResponse>().ReverseMap();
            CreateMap<CreateCustomerCommandRequest, Domain.Entities.Customer>().ReverseMap();
            CreateMap<CreateServiceCommandRequest, Domain.Entities.Service>().ReverseMap();
            CreateMap<CreateReviewsCommandRequest, Domain.Entities.Reviews>().ReverseMap();
            CreateMap<Reviews, Features.CQRS.Reviews.Queries.GetByHotelId.GetReviewsByHotelIdQueriesResponse>().ReverseMap();
            CreateMap<HotelCategory, Features.CQRS.HotelCategory.Queries.GetAll.GetAllHotelCategoryQueriesResponse>().ReverseMap();
            CreateMap<Rooms,Features.CQRS.Rooms.Command.Create.CreateRoomsCommandRequest>().ReverseMap();
            CreateMap<Domain.Entities.HotelsPoliticy, Features.CQRS.HotelsPoliticy.Command.Create.CreateHotelsPoliticyCommandRequest>().ReverseMap();
            CreateMap<Domain.Entities.HotelsPoliticy, Features.CQRS.HotelsPoliticy.Queries.GetAll.GetAllHotelsPoliticyQueriesResponse>().ReverseMap();
            CreateMap<HowFarSpecialPlace,CreateHowFarSpecialPlaceCommandRequest>().ReverseMap();
            CreateMap<HowFarSpecialPlace, Features.CQRS.HowFarSpecialPlace.Queries.GetAllByHotelId.GetAllHowFarSpecialPlaceQueriesResponse>().ForMember(destinationMember: x=> x.SpecialPlaceCategoryName, memberOptions: opt => opt.MapFrom(x=> x.SpecialPlaceCategory)).ReverseMap().ForMember(destinationMember: x=> x.SpecialPlaceCategory , memberOptions:opt=> opt.Ignore());
            CreateMap<CreateBillsCommandRequest, Bills>().ReverseMap();
            CreateMap<Bills,GetAllBillsQueriesResponse>().ForMember(destinationMember:x => x.MethodName,memberOptions:y=> y.MapFrom(x=> x.PaymetMethod)).ForMember(destinationMember: x => x.TimingName, memberOptions: y => y.MapFrom(x => x.PaymentTiming)).ForMember(destinationMember:x=> x.HotelName, memberOptions: opt=> opt.MapFrom(x=> x.Hotels)).ReverseMap();
            CreateMap<Bills, GetAllBillsByHotelIdQueriesResponse>().ForMember(destinationMember: x => x.MethodName, memberOptions: y => y.MapFrom(x => x.PaymetMethod)).ForMember(destinationMember: x => x.TimingName, memberOptions: y => y.MapFrom(x => x.PaymentTiming)).ForMember(destinationMember: x => x.HotelName, memberOptions: opt => opt.MapFrom(x => x.Hotels)).ReverseMap();
            CreateMap<HotelInformation, Features.CQRS.HotelInformation.Command.Create.CreateHotelInformationCommandRequest>().ReverseMap();
            CreateMap<HotelInformation,GetHotelInformationByIdQueriesResponse>().ReverseMap();
            CreateMap<HotelServices,Features.CQRS.HotelsService.Queries.GetAll.GetAllHotelsServiceQueriesResponse>().ForMember(destinationMember: x=> x.ServiceName,memberOptions:opt => opt.MapFrom(x=>x.Service)).ForMember(destinationMember:x=> x.IsNeedAFee,memberOptions:opt=> opt.MapFrom(y => y.Service)).ReverseMap();
            CreateMap<Neighborhood,GetAllNeighborhoodQueriesResponse>().ReverseMap();
            CreateMap<GetAllReservationByHotelIdQueriesResponse, Domain.Entities.Reservation>().ReverseMap();
            CreateMap<CreateHotelImagesCommandRequest,HotelImages>().ReverseMap();
            CreateMap<GetAllHotelImagesByIdQueriesResponse, HotelImages>().ReverseMap();
        }
    }
}
