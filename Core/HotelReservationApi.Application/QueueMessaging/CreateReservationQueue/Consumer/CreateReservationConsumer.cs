using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.Consumer
{
    public class CreateReservationConsumer : IAsyncDisposable
    {
        private readonly IUnitOfWork unitOfWork;
        private IConnection connection;
        private IChannel channel;

        public CreateReservationConsumer(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask DisposeAsync()
        {
            if (channel != null)
            {
                await channel.CloseAsync();
                await channel.DisposeAsync();
            }
            if (connection != null)
            {
               await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }

        public async Task StartConsume()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = await factory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "CreateReservationQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                var reservationData = System.Text.Json.JsonSerializer.Deserialize<Reservation>(message);
                await unitOfWork.writeRepository<Reservation>().AddAsync(reservationData);
                await unitOfWork.SaveAsync();

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };
            await channel.BasicConsumeAsync(queue: "CreateReservationQueue",
                                     autoAck: false,
                                     consumer: consumer);
        }
    }
}
