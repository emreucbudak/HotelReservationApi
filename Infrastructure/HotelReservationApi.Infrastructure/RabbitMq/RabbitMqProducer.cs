using HotelReservationApi.Application.RabbitMq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using RabbitMQ.Client;
using Stripe.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.RabbitMq
{
    public class RabbitMqProducer : IMessageQueueService, IHostedService, IAsyncDisposable
    {
        private readonly ConnectionFactory factory;
        private IChannel _channel;
        private IConnection connection;
        private readonly ILogger<RabbitMqProducer> logger;
        public RabbitMqProducer(IOptions<RabbitMqSettings> rabbit, ILogger<RabbitMqProducer> logger)
        {
            var settings = rabbit.Value;
            this.factory = new()
            {
                HostName = settings.HostName,
                Port = settings.Port,
                UserName = settings.Username,
                Password = settings.Password,
                AutomaticRecoveryEnabled = true
            };
            this.logger = logger;
        }



        public async Task PublishAsync<T>(string queueName, T message) where T : class
        {
            if (_channel == null || !_channel.IsOpen)
            {
                logger.LogError("RabbitMQ kanalı kullanıma hazır değil (Başlatılmadı veya Koptu). Mesaj gönderilemedi: {QueueName}", queueName);
                throw new InvalidOperationException("RabbitMQ kanalı kullanıma hazır değil. Lütfen servisin başlatıldığından emin olun.");
            }
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

                 await _channel.QueueDeclareAsync(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

              await   _channel.BasicPublishAsync(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: new BasicProperties() { Persistent = true},
                                     body: body,
                                     mandatory:true);
            

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("RabbitMQ bağlantısı  başlatılıyor...");

            try
            {
                connection = await factory.CreateConnectionAsync(cancellationToken);
                _channel = await connection.CreateChannelAsync();
                logger.LogInformation("RabbitMQ bağlantısı ve kanal başarıyla kuruldu.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RabbitMQ bağlantısı kurulamadı!");
            }
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("RabbitMQ bağlantısı durduruluyor ve kaynaklar serbest bırakılıyor...");
            await DisposeAsync(); 

        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_channel != null && _channel.IsOpen)
                {
                    await _channel.CloseAsync();
                    await _channel.DisposeAsync();
                    _channel = null;
                }
                if (connection != null && connection.IsOpen)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                    connection = null;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RabbitMQ bağlantısı kapatılamadı!");
            }
        }
    }
}
