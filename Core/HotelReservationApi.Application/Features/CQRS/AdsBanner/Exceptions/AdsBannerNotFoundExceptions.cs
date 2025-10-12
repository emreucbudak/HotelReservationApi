using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.AdsBanner.Exceptions
{
    public class AdsBannerNotFoundExceptions : NotFoundExceptions
    {
        public AdsBannerNotFoundExceptions(int id) : base($"{id}'e sahip ads reklam bannerı bulunamadı!")
        {
        }
    }   
    }
