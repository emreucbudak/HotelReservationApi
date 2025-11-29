using Stripe;

namespace HotelReservationApi.Application.Payment
{
    public interface IStripeService
    {
        Task<PaymentIntent> CreatePayment (decimal price,string currency,string paymentType,string paymentTiming);
    }
}
