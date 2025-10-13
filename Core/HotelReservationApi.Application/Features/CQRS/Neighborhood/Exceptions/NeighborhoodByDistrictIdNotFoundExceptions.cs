using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Neighborhood.Exceptions
{
    public class NeighborhoodByDistrictIdNotFoundExceptions : NotFoundExceptions
    {
        public NeighborhoodByDistrictIdNotFoundExceptions(int id) : base($"{id}'e sahip ilçenin mahalleleri bulunamadı!")
        {
        }
    }
}
