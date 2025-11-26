using HotelReservationApi.Application.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.RabbitMq
{
    public class RabbitMqProducer : IMessageQueueService
    {
        public Task PublishAsync<T>(string queueName, T message) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
