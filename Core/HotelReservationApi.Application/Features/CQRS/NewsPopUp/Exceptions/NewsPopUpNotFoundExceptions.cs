using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.NewsPopUp.Exceptions
{
    public class NewsPopUpNotFoundExceptions : NotFoundExceptions
    {
        public NewsPopUpNotFoundExceptions(int id) : base($"{id}'e sahip haber popup u bulunamadı")
        {
        }
    }
}
