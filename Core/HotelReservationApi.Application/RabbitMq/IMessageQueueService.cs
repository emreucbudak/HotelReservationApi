using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.RabbitMq
{
    public interface IMessageQueueService
    {
        Task PublishAsync<T>(string queueName, T message) where T : class;
    }
}
