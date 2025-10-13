using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.TypesFeatures.Exceptions
{
    public class TypesFeaturesNotFoundExceptions : NotFoundExceptions
    {
        public TypesFeaturesNotFoundExceptions(int id) : base($"{id}'e sahip oda özelliği bulunamadı!")
        {
        }
    }
}
