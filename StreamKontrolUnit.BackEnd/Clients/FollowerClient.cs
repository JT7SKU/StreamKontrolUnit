using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using StreamKontrolUnit.BackEnd.Hubs;

namespace StreamKontrolUnit.BackEnd.Clients
{
    public class FollowerClient
    {
        public FollowerClient(IHubContext<StreamKontrolUnitHub> followerHubContext)
        {
            this.FollowerHubContext = followerHubContext;
        }
        private IHubContext<StreamKontrolUnitHub> FollowerHubContext { get; }

        public void UpdateFollowers (int Followers)
        {
            FollowerHubContext.Clients.Group("followers").SendAsync("OnFollowerCountUpdated",Followers);
        }
    }
}
