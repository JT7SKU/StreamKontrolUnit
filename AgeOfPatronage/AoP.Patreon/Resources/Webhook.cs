using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AoP.Patreon.Resources
{
    public class Webhook
    {
        [JsonProperty("triggers")]
        public ICollection Triggers { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("paused")]
        public bool Paused { get; set; }
        [JsonProperty("last_attempted_at")]
        public string LastAttemptedAt { get; set; }
        [JsonProperty("num_consecutive_times_failed")]
        public int NumConsecutiveTimesFailed { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
    public class WebhookRelationships
    {
        [JsonProperty("client")]
        public OAuthClient client { get; set; }
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
    }
}
