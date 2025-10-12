using HotelReservationApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Exceptions
{
    public class BillsNotFoundExceptions : NotFoundExceptions
    {
        public BillsNotFoundExceptions(int id) : base($"{id}'e sahip otel hakkında fatura bulunamadı!")
        {
        }
    }
}
