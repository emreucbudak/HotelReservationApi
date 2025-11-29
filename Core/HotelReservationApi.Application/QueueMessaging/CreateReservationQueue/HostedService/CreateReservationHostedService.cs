using HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.Consumer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.HostedService
{
    public class CreateReservationHostedService : IHostedService
    {
        private readonly CreateReservationConsumer _createReservationConsumer;

        public CreateReservationHostedService(CreateReservationConsumer createReservationConsumer)
        {
            _createReservationConsumer = createReservationConsumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _createReservationConsumer.StartConsume();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_createReservationConsumer is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
        }
    }
}
