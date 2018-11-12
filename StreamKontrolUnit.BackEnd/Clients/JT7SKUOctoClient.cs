using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using StreamKontrolUnit.BackEnd.Hubs;

namespace StreamKontrolUnit.BackEnd.Clients
{
    public class JT7SKUOctoClient
    {
        public JT7SKUOctoClient(IHubContext<JT7SKUOctoHub>octoSKUcontext)
        {
            this.OctoSKUcontext = octoSKUcontext;
        }

        private IHubContext<JT7SKUOctoHub> OctoSKUcontext { get; }

        internal void UpdateGitHub(string repository, string username, int commits)
        {
            OctoSKUcontext.Clients.Group("github").SendAsync("OnGitHubUpdated", repository, username, commits);
        }
    }
}
