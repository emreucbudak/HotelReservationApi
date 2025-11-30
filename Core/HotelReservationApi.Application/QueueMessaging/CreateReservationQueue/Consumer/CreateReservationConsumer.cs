using HotelReservationApi.Application.DTOS;
using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create.CreateAfterBill;
using HotelReservationApi.Application.RabbitMq.Settings;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.Consumer
{
    public class CreateReservationConsumer : IAsyncDisposable
    {
        private IConnection connection;
        private IChannel channel;
        private readonly IMediator mediator;
        private readonly RabbitMqSettings rabbitMqSettings;


        public CreateReservationConsumer(IMediator mediator,IOptions<RabbitMqSettings> rabbitmq)
        {
            this.mediator = mediator;
            rabbitMqSettings = rabbitmq.Value;
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

                var customerDtos = reservationData.Customer
                    .Select(c => new CustomerDTO
                    {
                        Name = c.Name,
                        Surname = c.Surname,
                        BirthDate = c.BirthDate,
                        GenderId = c.GenderId,
                    })
                    .ToList();
                var reservationRooms = reservationData.reservationRooms
                    .Select(rr => new ReservationRoomDTO
                    {
                        RoomId = rr.RoomId,
                        ReservationId = rr.ReservationId,
                    })
                    .ToList();
                await mediator.Send(new Features.CQRS.Reservation.Command.Create.CreateAfterBill.CreateReservationAfterBillCommandRequest
                {
                    MemberId = reservationData.Member.Id,
                    HotelsId = reservationData.Hotels.Id,
                    StartDate = reservationData.StartDate,
                    EndDate = reservationData.EndDate,
                    TotalPrice = reservationData.TotalPrice,
                    ReservationDate = reservationData.ReservationDate,
                    customerDto = customerDtos,
                    ReservationRooms = reservationRooms
                });


                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };
            await channel.BasicConsumeAsync(queue: "CreateReservationQueue",
                                     autoAck: false,
                                     consumer: consumer);
        }
    }
}
