using HotelReservationApi.Application.Payment;
using Stripe;

namespace HotelReservationApi.Infrastructure.Payment
{
    public class StripeService : IStripeService
    {
        public async Task<PaymentIntent> CreatePayment(decimal price, string currency, string paymentType,string paymentTiming)
        {
            long amountInCents = (long)(price * 100m);
            var pay = new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = currency,
                PaymentMethod = "pm_card_visa",
                PaymentMethodTypes = new List<string> { "card" },
                Description = "Hotel reservasyon ödemesi : " + paymentTiming + " " + "Kart Türü" + paymentType,
                Confirm = true

            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(pay);
            return paymentIntent;


        }
    }
}
