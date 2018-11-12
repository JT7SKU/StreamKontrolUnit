using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using StreamKontrolUnit.BackEnd.Hubs;

namespace StreamKontrolUnit.BackEnd.Clients
{
    public class ViewerClient
    {
        public ViewerClient(IHubContext<StreamKontrolUnitHub> viewerHubContext)
        {
            this.ViewerHubContext = viewerHubContext;
        }
        private IHubContext<StreamKontrolUnitHub> ViewerHubContext { get; }

        public void UpdateViewers(string serviceName,int ViewerCount)
        {
            ViewerHubContext.Clients.Group("Viewers").SendAsync("OnViewersCountUpdated", serviceName.ToLowerInvariant(),ViewerCount);
        }
    }
}
