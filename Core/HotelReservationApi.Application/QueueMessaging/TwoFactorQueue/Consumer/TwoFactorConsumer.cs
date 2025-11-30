using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.RabbitMq.Models;
using HotelReservationApi.Application.RabbitMq.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Consumer
{
    public class TwoFactorConsumer : IAsyncDisposable
    {
        private readonly IEmailService _emailService;
        private  IConnection connection;
        private  IChannel channel;
        private readonly RabbitMqSettings rabbitMqSettings;
        public TwoFactorConsumer(IEmailService emailService,IOptions<RabbitMqSettings> rabbit)
        {
            _emailService = emailService;
            rabbitMqSettings = rabbit.Value;
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
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = rabbitMqSettings.Username, Password = rabbitMqSettings.Password, AutomaticRecoveryEnabled = true };
            connection = await factory.CreateConnectionAsync();
              channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "TwoFactorQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailMessage = System.Text.Json.JsonSerializer.Deserialize<TwoFactorMessage>(message);
                if (emailMessage != null)
                {
                    await _emailService
                    .Builder()
                    .To(emailMessage.Email)
                    .Subject("Dogrulama Kodu")
                    .Body(null,emailMessage.VerificationCode)
                    .SendAsync();
                }
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };
            await channel.BasicConsumeAsync(queue: "TwoFactorQueue"
    , autoAck: false,
    consumer: consumer);
        }
    }
}
