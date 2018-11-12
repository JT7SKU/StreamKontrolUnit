using SKU.Stream.Services.Models.GitHub;
using StreamKontrolUnit.BackEnd.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamKontrolUnit.BackEnd.Hubs
{
    public class JT7SKUOctoHub : BaseHub
    {
        public JT7SKUOctoClient JT7SKUOctoClient{get;}

        public JT7SKUOctoHub(JT7SKUOctoClient client)
        {
            this.JT7SKUOctoClient = client;
        }
        private void Git_Updated(object sender, GitHubContributorsEventArgs e)
        {
            this.JT7SKUOctoClient.UpdateGitHub(e.Repository, e.UserName, e.NewCommits);
        }

    }
}
