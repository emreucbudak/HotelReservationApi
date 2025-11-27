namespace HotelReservationApi.Application.Payment
{
    public interface IStripeService
    {
        Task<string> CreatePayment (int price,string currency,string paymentType);
    }
}
