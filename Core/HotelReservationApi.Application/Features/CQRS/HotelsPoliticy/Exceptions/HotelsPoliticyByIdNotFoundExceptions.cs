using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Exceptions
{
    public class HotelsPoliticyByIdNotFoundExceptions : NotFoundExceptions
    {
        public HotelsPoliticyByIdNotFoundExceptions(int id) : base($"{id}'e sahip hotelin politikaları bulunamadı")
        {
        }
    }
}
