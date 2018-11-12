using System;
using System.Collections.Generic;
using System.Text;

namespace StreamKernelUnit
{
    public class ChatUserInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Name of originating service
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Service specific properties (user roles etc)
        /// </summary>
        public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public uint ChannelId { get; set; }
        public uint UserId { get; set; }
        public string UserName { get; set; }
    }
}
