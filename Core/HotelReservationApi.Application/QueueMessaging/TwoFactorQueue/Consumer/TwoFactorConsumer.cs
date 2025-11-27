using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Consumer
{
    public class TwoFactorConsumer : IAsyncDisposable
    {
        private readonly IEmailService _emailService;
        private  IConnection connection;
        private  IChannel channel;
        public TwoFactorConsumer(IEmailService emailService)
        {
            _emailService = emailService;
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
                    .To(emailMessage.Email)
                    .Subject("Dogrulama Kodu")
                    .Body(null,emailMessage.Code)
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
