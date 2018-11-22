using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SKU.Stream.Services;
using StreamKernelUnit;
using StreamKontrolUnit.BackEnd.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamKontrolUnit.BackEnd.Hubs
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class StreamKontrolUnitHub : BaseHub
    {
        private StreamService StreamService { get; }
        private FollowerClient FollowerClient { get; }
        private StreamKontrolUnitClient StreamKontrolUnitClient { get; }
        private ViewerClient ViewerClient { get; }

        public StreamKontrolUnitHub(StreamService streamService, FollowerClient followerClient, StreamKontrolUnitClient streamKontrolUnitClient, ViewerClient viewerClient)
        {
            this.StreamService = streamService;
            this.StreamKontrolUnitClient = streamKontrolUnitClient;
            this.FollowerClient = followerClient;
            this.ViewerClient = viewerClient;
            StreamService.Updated += StreamService_Updated;
        }

        private void StreamService_Updated(object sender, ServiceUpdatedEventArgs e)
        {
            if (e.NewFollowers.HasValue)
            {
                this.FollowerClient.UpdateFollowers(StreamService.CurrentFollowerCount + e.NewFollowers.Value);
            }
            if (e.NewViewers.HasValue)
            {
                this.ViewerClient.UpdateViewers(e.ServiceName,e.NewViewers.Value);
            }
        }
    }
}
