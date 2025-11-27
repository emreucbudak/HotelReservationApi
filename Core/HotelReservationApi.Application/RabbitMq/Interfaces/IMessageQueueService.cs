namespace HotelReservationApi.Application.RabbitMq.Interfaces
{
    public interface IMessageQueueService
    {
        Task PublishAsync<T>(string queueName, T message) where T : class;
    }
}
