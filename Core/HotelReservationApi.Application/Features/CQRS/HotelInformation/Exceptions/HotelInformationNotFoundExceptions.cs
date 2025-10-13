using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelInformation.Exceptions
{
    public class HotelInformationNotFoundExceptions : NotFoundExceptions
    {
        public HotelInformationNotFoundExceptions(int id) : base($"{id}'e sahip hotel bilgisi bulunamadı!")
        {
        }
    }
}
