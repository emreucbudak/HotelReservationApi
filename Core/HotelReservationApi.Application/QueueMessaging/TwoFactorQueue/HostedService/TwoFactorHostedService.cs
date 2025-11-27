using HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Consumer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.HostedService
{
    public class TwoFactorHostedService : IHostedService
    {
        private readonly TwoFactorConsumer _twoFactorConsumer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
