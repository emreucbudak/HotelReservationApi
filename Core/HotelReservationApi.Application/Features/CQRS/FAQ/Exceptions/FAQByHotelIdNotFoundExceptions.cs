using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Exceptions
{
    public class FAQByHotelIdNotFoundExceptions : NotFoundExceptions
    {
        public FAQByHotelIdNotFoundExceptions(int id) : base($"{id}'e sahip otelinize ait SSS bulunamadı")
        {
        }
    }
}
