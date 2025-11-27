using HotelReservationApi.Application.Payment;
using Stripe;

namespace HotelReservationApi.Infrastructure.Payment
{
    public class StripeService : IStripeService
    {
        public async Task<string> CreatePayment(int price, string currency, string paymentType)
        {
            var pay = new PaymentIntentCreateOptions
            {
                Amount = price,
                Currency = currency,
                PaymentMethod =  "Card",
                Description = "Hotel reservasyon ödemesi : " + paymentType,
                

            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(pay);
            var answer = await  Task.FromResult(paymentIntent.ClientSecret);
            return answer;


        }
    }
}
