using System;
using System.Collections.Generic;
using System.Text;

namespace N7Functions.Models
{
    public class SettingsMessage
    {
        public string Stage { get; set; }
        public string SiteUrl { get; set; }
        public string StorageUrl { get; set; }
        public string ContainerSAS { get; set; }
        public string InputContainerName { get; set; }
        public string OutputContainerName { get; set; }
    }
}
