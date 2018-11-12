using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SKU.Stream.Services.Models.GitHub;
using StreamKontrolUnit.BackEnd.Hubs;

namespace StreamKontrolUnit.BackEnd.Clients
{
    public class StreamKontrolUnitClient
    {
        public StreamKontrolUnitClient(IHubContext<StreamKontrolUnitHub> streamHubContext)
        {
            this.StreamHubContext = streamHubContext;
        }
        private IHubContext<StreamKontrolUnitHub> StreamHubContext { get; }

        public void UpdateFollowers(int Followers)
        {
            StreamHubContext.Clients.Group("followers").SendAsync("OnFollowerCountUpdated", Followers);
        }

        public void UpdateViewers(string serviceName, int ViewerCount)
        {
            StreamHubContext.Clients.Group("Viewers").SendAsync("OnViewersCountUpdated", serviceName.ToLowerInvariant(), ViewerCount);
        }

        internal void UpdateGitHub(IEnumerable<GitHubInformation> contributors)
        {
            StreamHubContext.Clients.Group("github").SendAsync("GitHubUpdated", contributors);
        }
    }
}
