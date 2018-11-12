using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WikiaLib.Models
{
    public partial class Creator
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public partial class OriginalDimensions
    {
        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }
    }
}
