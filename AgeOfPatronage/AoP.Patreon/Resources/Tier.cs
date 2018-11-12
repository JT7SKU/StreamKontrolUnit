using Newtonsoft.Json;
using System.Collections.Generic;

namespace AoP.Patreon.Resources
{
    public class Tier
    {
        [JsonProperty("amount_cents")]
        public int AmounthCents { get; set; }
        [JsonProperty("user_limit")]
        public int UserLimit { get; set; }
        [JsonProperty("remaining")]
        public int Remaining { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("requires_shipping")]
        public bool RequiresShipping { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("patron_count")]
        public int PatronCount { get; set; }
        [JsonProperty("post_count")]
        public int PostCount { get; set; }
        [JsonProperty("discord_role_ids")]
        public string DiscordRoleIds { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("edited_at")]
        public string EditedAt { get; set; }
        [JsonProperty("published")]
        public bool Published { get; set; }
        [JsonProperty("published_at")]
        public string PublishedAt { get; set; }
        [JsonProperty("unpublished_at")]
        public string UnPublishedAt { get; set; }
    }
    public class TierRelationship
    {
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
        [JsonProperty("tier_image")]
        public Media TierImage { get; set; }
        [JsonProperty("benefits")]
        public List<Benefit> Benefits { get; set; }
    }
}