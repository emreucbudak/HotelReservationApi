using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Exception
{
    public class HotelImagesNotFoundException : NotFoundExceptions
    {
        public HotelImagesNotFoundException(int id) : base($"{id}'e ait otelin resimleri bulunamadı!")
        {
        }
    }
}
