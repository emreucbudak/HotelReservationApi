using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Neighborhood.Exceptions
{
    public class NeighborhoodNotFoundExceptions : NotFoundExceptions
    {
        public NeighborhoodNotFoundExceptions(int id) : base($"{id}'e sahip mahalle bulunamadı!")
        {
        }
    }
}
