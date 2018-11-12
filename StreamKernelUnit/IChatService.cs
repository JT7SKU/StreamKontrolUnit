﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreamKernelUnit
{
    public interface IChatService
    {
        string Name { get; }
        bool IsAuthenticated { get; }

        event EventHandler<ChatMessageEventArgs> ChatMessage;
        event EventHandler<ChatUserInfoEventArgs> UserJoined;
        event EventHandler<ChatUserInfoEventArgs> UserLeft;

        Task<bool> SendMessageAsync(string message);
        Task<bool> SendWhisperAsync(string userName,string message);
        Task<bool> TimeoutUserAsync(string userName ,TimeSpan time);
        Task<bool> BanUserAsync(string userName);
        Task<bool> UnbanUserAsync(string userName);
    }
}
