using AgeOfPatronage.Patreon.TriggersV2;
using AoP.Patreon.Resources;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AgeOfPatronage.Patreon
{
   public  class Proxy : IDisposable
    {
        private ConfigurationSettings Settings;
        public const string LOGGER_CATEGORY = "Patreon";
        private ILogger Logger { get; }
        private HttpClient Client { get; set; }

        public event EventHandler<CreateMembersEventArgs> NewMember;
        public event EventHandler<CreateMembersPledgeEventArgs> MemberNewPledge;

        public Proxy(HttpClient client, IOptions<ConfigurationSettings> settings, ILoggerFactory loggerFactory) :
            this(client, settings.Value,loggerFactory.CreateLogger("LOGGER_CATEGORY"))
        {

        }
        internal Proxy(HttpClient client,ConfigurationSettings settings, ILogger logger)
        {
            Settings = settings;
            Logger = logger;
        }
        private void ConfigureClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.patreon.com/oauth2");
            client.DefaultRequestHeaders.Add("Client-Id", Settings.ClientId);
            this.Client = client;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
