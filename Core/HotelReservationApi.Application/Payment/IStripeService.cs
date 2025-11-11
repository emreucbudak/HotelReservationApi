using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Payment
{
    public interface IStripeService
    {
        Task<string> CreatePayment (int price,string currency,string paymentType);
    }
}
