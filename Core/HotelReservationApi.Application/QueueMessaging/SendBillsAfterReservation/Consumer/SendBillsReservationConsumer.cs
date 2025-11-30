using HotelReservationApi.Application.PdfWriter;
using HotelReservationApi.Application.RabbitMq.Models;
using HotelReservationApi.Application.RabbitMq.Settings;
using HotelReservationApi.Domain.Entities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.QueueMessaging.SendBillsAfterReservation.Consumer
{
    public class SendBillsReservationConsumer : IAsyncDisposable
    {
        private  IChannel channel;
        private  IConnection connection;
        private readonly IPdfWriter _pdfWriter;
        private readonly RabbitMqSettings rabbitMqSettings;

        public SendBillsReservationConsumer(IPdfWriter pdfWriter,IOptions<RabbitMqSettings> rabbit)
        {
            _pdfWriter = pdfWriter;
            rabbitMqSettings = rabbit.Value;
        }

        public async ValueTask DisposeAsync()
        {
            if (channel != null)
            {
                await channel.CloseAsync();
                await channel.DisposeAsync();
                channel = null;
            }
            if (connection != null)
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
                connection = null;
            }
        }
        public async Task StartConsume()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = rabbitMqSettings.Username, Password = rabbitMqSettings.Password, AutomaticRecoveryEnabled = true };
            connection = await factory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "SendBillsAfterReservationQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var reservationData = System.Text.Json.JsonSerializer.Deserialize<BillPdfModel>(message);
                if (reservationData != null)
                {
                    await _pdfWriter.WriteBillPdf(reservationData);
                }
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };


        }
    }
}
