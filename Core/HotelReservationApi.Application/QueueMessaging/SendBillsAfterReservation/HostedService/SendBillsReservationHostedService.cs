using HotelReservationApi.Application.QueueMessaging.SendBillsAfterReservation.Consumer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.QueueMessaging.SendBillsAfterReservation.HostedService
{
    public class SendBillsReservationHostedService : IHostedService
    {
        private readonly SendBillsReservationConsumer consumer;

        public SendBillsReservationHostedService(SendBillsReservationConsumer consumer)
        {
            this.consumer = consumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await consumer.StartConsume();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if(consumer is IAsyncDisposable asyncDisposable)
            {
                await  asyncDisposable.DisposeAsync();
            }
        }
    }
}
