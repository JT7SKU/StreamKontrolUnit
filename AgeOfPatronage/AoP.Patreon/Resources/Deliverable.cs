using Newtonsoft.Json;
using System;

namespace AoP.Patreon.Resources
{
    public class Deliverable
    {
        [JsonProperty("completed_at")]
        public DateTime CompletedAt { get; set; }
        [JsonProperty("delivery_status")]
        public string DeliveryStatus { get; set; } 
        [JsonProperty("due_at")]
        public DateTime DueAt { get; set; }
    }
    public class DeliverableRelationship
    {
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
        [JsonProperty("benefit")]
        public Benefit Benefit { get; set; }
        [JsonProperty("member")]
        public Member Member { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
    }
}