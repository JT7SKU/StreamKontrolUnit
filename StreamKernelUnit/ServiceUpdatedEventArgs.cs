using System;
using System.Collections.Generic;
using System.Text;

namespace StreamKernelUnit
{
    public class ServiceUpdatedEventArgs
    {
        public string ServiceName { get; set; }
        public uint ChannelId { get; set; }
        public int? NewFollowers { get; set; }
        public int? NewViewers { get; set; }
        public bool? IsOnline { get; set; }
    }
}
