using Microsoft.AspNetCore.SignalR.Client;
using SKU.Stream.Services.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SKU.Stream.Services
{
    public class OctoClient
    {
        public HubConnection connection;
        private GitHubContributorsEventArgs message;
        public OctoClient()
        {
            ConnectOctoHub();
        }

        private async Task ConnectOctoHub()
        {
            connection = new HubConnectionBuilder().WithUrl("/hubs/jt7skuoctohub").Build();
            connection.On<GitHubContributorsEventArgs>("UpdateGitHub", UpdateGitHub);
            connection.On<GitHubContributorsEventArgs>("UpdateGitHub", this.UpdateGitHub);
            await connection.StartAsync();
        }
        public void UpdateGitHub(GitHubContributorsEventArgs e)
        {
            this.message = e;
            SendAsync();
        }
        private async Task SendAsync()
        {
            await connection.InvokeAsync("UpdateGithub", message);

        }
    }
}
