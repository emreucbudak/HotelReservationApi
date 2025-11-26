using HotelReservationApi.Application.RabbitMq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.RabbitMq
{
    public class RabbitMqProducer : IMessageQueueService
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly IModel _channel;
        private readonly IConnection connection;
        public RabbitMqProducer(IOptions<RabbitMqSettings> rabbit)
        {
            connectionFactory = new()
            {
                HostName = rabbit.Value.HostName,
                UserName = rabbit.Value.Username,
                Password = rabbit.Value.Password,
                Port = rabbit.Value.Port,
            };
            connection = await connectionFactory.CreateConnectionAsync();

        }

        public Task PublishAsync<T>(string queueName, T message) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
