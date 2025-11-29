using HotelReservationApi.Application.Emails;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.Consumer
{
    public class CreateReservationConsumer
    {
        private readonly IEmailService _emailService;
        private IConnection connection;
        private IChannel channel;

        public CreateReservationConsumer(IEmailService emailService, IConnection connection, IChannel channel)
        {
            _emailService = emailService;
            this.connection = connection;
            this.channel = channel;
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

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };
            await channel.BasicConsumeAsync(queue: "CreateReservationQueue",
                                     autoAck: false,
                                     consumer: consumer);
        }
    }
}
