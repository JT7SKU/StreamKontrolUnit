using Microsoft.Extensions.Hosting;
using StreamKernelUnit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SKU.Stream.Services
{
    public class OBSService : IHostedService, IStreamService
    {

        public string Name => throw new NotImplementedException();

        public int CurrentFollowerCount => throw new NotImplementedException();

        public int CurrentViewerCount => throw new NotImplementedException();

        public event EventHandler<ServiceUpdatedEventArgs> Updated;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TimeSpan?> UpTime()
        {
            throw new NotImplementedException();
        }
    }
}
