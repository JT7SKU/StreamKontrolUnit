using System;
using System.Collections.Generic;
using System.Text;

namespace SKU.Stream.Twitch
{
    public class ChatUserJoinedEventArgs : EventArgs
    {
        public string UserName { get; set; }
    }
}
