using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SKU.Stream.Twitch;
using StreamKernelUnit;

namespace SKU.Stream.Services
{
     public class TwitchService : IHostedService, IStreamService, IChatService
    {
        private IConfiguration Configuration { get; }
        private ILogger Logger { get; }

        private readonly Proxy Proxy;
        private ChatClient _ChatClient;

        public event EventHandler<ServiceUpdatedEventArgs> Updated;
        public event EventHandler<ChatMessageEventArgs> ChatMessage;
        public event EventHandler<ChatUserInfoEventArgs> UserJoined;
        public event EventHandler<ChatUserInfoEventArgs> UserLeft
        {
            add { }
            remove { }
        }

        public TwitchService(IConfiguration config, ILoggerFactory loggerFactory, Proxy proxy, ChatClient chatClient)
        {
            this.Configuration = config;
            this.Logger = loggerFactory.CreateLogger("StreamServices");
            this.Proxy = proxy;
            this._ChatClient = chatClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StartTwitchMonitoring();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return StopTwitchMonitoring();
        }

        public static int _CurrentFollowerCount;
        public int CurrentFollowerCount { get { return _CurrentFollowerCount; } internal set { _CurrentFollowerCount = value; } }
        public static int _CurrentViewerCount;
        public int CurrentViewerCount{get{ return _CurrentViewerCount; } }
        public string Name { get { return "Twitch"; } }
        public ValueTask<TimeSpan?> UpTime() => Proxy.UpTime();
        public bool IsAuthenticated => true;

        private async Task StartTwitchMonitoring()
        {
            try
            {
                _ChatClient.Connected += (c, args) => Logger.LogInformation("Now connected to Twitch Chat");
                _ChatClient.NewMessage += _ChatClient_NewMessage;
                _ChatClient.UserJoined += _ChatClient_UserJoined;
                _ChatClient.Init();

                _CurrentViewerCount = await Proxy.GetFollowersCountAsync();
                Proxy.NewFollowers += Proxy_NewFollowers;
                Proxy.WatchFollowers(10000);

                _CurrentViewerCount = await Proxy.GetFollowersCountAsync();
                Proxy.NewViewers += Proxy_NewViewers;
                Proxy.WatchViewers();

                Logger.LogInformation($"Now monitoring Twitch with {_CurrentFollowerCount} followers and {_CurrentViewerCount} Viewers");
            }
            catch (Exception ex)
            {

                Logger.LogWarning($"Start TwitchMonitoring failed {ex.Message}");
            }
        }

        private void Proxy_NewFollowers(object sender, NewFollowersEventArgs e)
        {
            Interlocked.Exchange(ref _CurrentFollowerCount, e.FollowerCount);
            Logger.LogInformation($"New Followers on Twitch, new total: {_CurrentFollowerCount}");

            Updated?.Invoke(this, new ServiceUpdatedEventArgs
            {
                ServiceName = Name,
                NewFollowers = _CurrentFollowerCount
            });
        }

        private void _ChatClient_UserJoined(object sender, ChatUserJoinedEventArgs e)
        {
            UserJoined?.Invoke(this, new ChatUserInfoEventArgs
            {
                ServiceName = "Twitch",
                UserName = e.UserName
            });
        }

        private void _ChatClient_NewMessage(object sender, NewMessageEventArgs e)
        {
            ChatMessage?.Invoke(this, new ChatMessageEventArgs
            {
                IsModerator = e.Badges?.Contains(@"moderator/1") ?? false,
                IsOwner = (_ChatClient.ChannelName == e.UserName),
                IsWhisper = e.IsWisper,
                Message = e.Message,
                ServiceName = "Twitch",
                UserName = e.UserName
            });
        }

        private void Proxy_NewViewers(object sender, NewViewerEventArgs e)
        {
            Interlocked.Exchange(ref _CurrentViewerCount, e.ViewerCount);
            Logger.LogInformation($"New Viewers on Twitch, new total: {_CurrentViewerCount}");

            Updated?.Invoke(this, new ServiceUpdatedEventArgs
            {
                ServiceName = Name,
                NewViewers = _CurrentViewerCount
            });
        }

        private Task StopTwitchMonitoring()
        {
            Proxy.Dispose();
            _ChatClient.Dispose();

            return Task.CompletedTask;
        }

        public Task<bool> SendMessageAsync(string message)
        {
            _ChatClient.PostMessage(message);
            return Task.FromResult(true);
        }

        public Task<bool> SendWhisperAsync(string userName, string message)
        {
            _ChatClient.WhisperMessage(message, userName);
            return Task.FromResult(true);
        }

        public Task<bool> TimeoutUserAsync(string userName, TimeSpan time)
        {
            //TODO Addinng User Timeout to ChatClient
            return Task.FromResult(false);
        }

        public Task<bool> BanUserAsync(string userName)
        {
            //TODO Addinng User Timeout to ChatClient
            return Task.FromResult(false);
        }

        public Task<bool> UnbanUserAsync(string userName)
        {
            //TODO Addinng User Timeout to ChatClient
            return Task.FromResult(false);

        }
        internal void MessageReceived(bool isModerator, bool isBroadcaster, string message, string userName)
        {
            ChatMessage?.Invoke(this, new ChatMessageEventArgs
            {
                IsModerator = isModerator,
                IsOwner = isBroadcaster,
                IsWhisper = false,
                Message = message,
                ServiceName = "Twitch",
                UserName = userName
            });
        }
    }
}
