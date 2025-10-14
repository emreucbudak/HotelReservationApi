using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Customer.Exceptions
{
    public class CustomerNotFoundExceptions : NotFoundExceptions
    {
        public CustomerNotFoundExceptions(int id) : base($"{id}'e sahip müşteri bulunamadı")
        {
        }
    }
}
