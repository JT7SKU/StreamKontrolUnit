using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AoP.Patreon
{
    public class PatreonClient
    {
        internal HttpClient PatronClient { get; private set; }
        private ILogger Logger { get; }

        public Task GetEndpoints(string endpoint)
        {
            return Task.CompletedTask;
        }
    }
}
