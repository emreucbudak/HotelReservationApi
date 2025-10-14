using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Exceptions
{
    public class HowFarSpecialGetByIdNotFoundExceptions : NotFoundExceptions
    {
        public HowFarSpecialGetByIdNotFoundExceptions(int id) : base($"{id}'e  sahip hotele ait  önemli konum bulunamadı!")
        {
        }
    }
    
    }

