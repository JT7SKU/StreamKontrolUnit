using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SKU.Stream.Services.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SKU.Stream.Services
{
    public class GitHubService : IHostedService
    {
        public static DateTime LastUpdate = DateTime.MinValue;
        public GitHubService(IServiceProvider services, ILogger<GitHubService> logger)
        {
            this.Services = services;
            this.Logger = logger;
        }

        public IServiceProvider Services { get; }
        public ILogger<GitHubService> Logger { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return MonitorUpdates(cancellationToken);
        }

        private async Task MonitorUpdates(CancellationToken cancellationToken)
        {
            var lastRequest = DateTime.Now;
            using (var scope = Services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetService(typeof(GitHubRepository)) as GitHubRepository;
                var OctoClient = scope.ServiceProvider.GetService(typeof(OctoClient)) as OctoClient;
                while (!cancellationToken.IsCancellationRequested)
                {
                    if(repo != null)
                    {
                        var lastUpdate = await repo.GetLastCommitTimestamp();
                        if(lastUpdate.Item1 > LastUpdate)
                        {
                            LastUpdate = lastUpdate.Item1;

                            Logger.LogWarning($"Triggering refresh of GitHub Scoreboard with update as of {lastUpdate}");
                            OctoClient?.UpdateGitHub(new GitHubContributorsEventArgs("", "", 0));

                        }
                    }
                    await Task.Delay(500, cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
