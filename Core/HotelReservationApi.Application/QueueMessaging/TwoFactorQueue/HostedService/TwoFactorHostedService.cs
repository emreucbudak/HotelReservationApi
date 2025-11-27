using HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Consumer;
using Microsoft.Extensions.Hosting;

namespace HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.HostedService
{
    public class TwoFactorHostedService : IHostedService
    {
        private readonly TwoFactorConsumer _twoFactorConsumer;

        public TwoFactorHostedService(TwoFactorConsumer twoFactorConsumer)
        {
            _twoFactorConsumer = twoFactorConsumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _twoFactorConsumer.StartConsume();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if(_twoFactorConsumer is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
        }
    }
}
