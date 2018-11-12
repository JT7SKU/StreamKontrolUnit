using Newtonsoft.Json;
using System;
namespace AoP.Patreon.Resources
{
    public class Goal
    {
        [JsonProperty("amount_cents")]
        public int AmountCents { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("ReachedAt")]
        public DateTime ReachedAt { get; set; }
        [JsonProperty("completed_percentage")]
        public int CompletedPercentage { get; set; }
    }
    public class GoalRelationships
    {
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
    }
}