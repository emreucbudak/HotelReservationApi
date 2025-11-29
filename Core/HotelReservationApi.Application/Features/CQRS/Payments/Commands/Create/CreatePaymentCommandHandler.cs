using HotelReservationApi.Application.Features.CQRS.Payments.Exception;
using HotelReservationApi.Application.Payment;
using HotelReservationApi.Application.RabbitMq.Interfaces;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;

namespace HotelReservationApi.Application.Features.CQRS.Payments.Commands.Create
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommandRequest>
    {
        private readonly IDistributedCache cache;
        private readonly IStripeService stripeService;
        private readonly IMessageQueueService queue;

        public CreatePaymentCommandHandler(IDistributedCache cache, IStripeService stripeService, IMessageQueueService queue)
        {
            this.cache = cache;
            this.stripeService = stripeService;
            this.queue = queue;
        }

        public async Task Handle(CreatePaymentCommandRequest request, CancellationToken cancellationToken)
        {
            bool isExist = cache.GetString(request.PaymentToken) is not null;
            if (!isExist)
            {
                throw new ReservationNotFoundExceptions();
            }
            var reservationData = cache.GetString(request.PaymentToken);
            var reservation = System.Text.Json.JsonSerializer.Deserialize<Domain.Entities.Reservation>(reservationData);
            string paymentMethod = request.PaymentMethodId == 1 ? "Kredi Kartı" : "Banka Kartı";
            reservation.TotalPrice = request.PaymentTimingId == 2
                ? reservation.TotalPrice * 0.1m
                : reservation.TotalPrice;
            string paymentTiming = request.PaymentTimingId == 1 ? "Hemen Öde" : "Otelde Öde";

            try
            {
                var paymentIntent = await stripeService.CreatePayment(reservation.TotalPrice, "usd", paymentMethod, paymentTiming);
                var paymentStatus = paymentIntent.Status is "succeeded" ? true : false;
                if (!paymentStatus)
                {
                    throw new PaymentFailedExceptions();
                }
                await queue.PublishAsync("CreateReservationQueue", new
                {
                    Reservation = reservation,
                    PaymentId = paymentIntent.Id,
                    Amount = paymentIntent.AmountReceived,
                    Currency = paymentIntent.Currency,
                    PaymentMethod = paymentMethod,
                    PaymentTiming = paymentTiming
                });
                await queue.PublishAsync("ReservationCreateQueue", reservation);
                await cache.RemoveAsync(request.PaymentToken);

            }
            catch 
            {
                throw;
            }
        }
    }
}