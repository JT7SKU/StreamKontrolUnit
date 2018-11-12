using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AoP.Patreon.Resources
{
    public class Campaign
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("creation_name")]
        public string CreationName { get; set; }
        [JsonProperty("pay_per_name")]
        public string PayPerName { get; set; }
        [JsonProperty("one_liner")]
        public string OneLiner { get; set; }
        [JsonProperty("main_video_embed")]
        public string MainVideoEmbed { get; set; }
        [JsonProperty("main_video_url")]
        public string MainVideoUrl { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("image_small_url")]
        public string ImageSmallUrl { get; set; }
        [JsonProperty("thanks_video_url")]
        public string ThanksVideoUrl { get; set; }
        [JsonProperty("thanks_embed")]
        public string ThanksEmbed { get; set; }
        [JsonProperty("thanks_msg")]
        public string ThanksMsg { get; set; }
        [JsonProperty("is_monthly")]
        public bool IsMonthly { get; set; }
        [JsonProperty("has_rss")]
        public bool HasRSS { get; set; }
        [JsonProperty("has_sent_rss_notify")]
        public bool HasSentRSSMontly { get; set; }
        [JsonProperty("rss_feed_title")]
        public string RssFeedTitle { get; set; }
        [JsonProperty("rss_artwork_url")]
        public string RssArtworkUrl { get; set; }
        [JsonProperty("is_nsfw")]
        public bool IsNSFW { get; set; }
        [JsonProperty("is_charged_immedieately")]
        public bool IsChargedImmediately { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
        [JsonProperty("pledge_url")]
        public string PledgeUrl { get; set; }
        [JsonProperty("patron_count")]
        public int PatronCount { get; set; }
        [JsonProperty("discord_server_id")]
        public string DiscordServerId { get; set; }
        [JsonProperty("google_analytics_id")]
        public string GoogleAnalyticsId { get; set; }
        [JsonProperty("earnings_visibility")]
        public string EarningsVisibility { get; set; }
    }
    public class CampaignRelationships
    {
        [JsonProperty("tiers")]
        public List<Tier> Tiers { get; set; }
        [JsonProperty("creator")]
        public User Creator { get; set; }
        [JsonProperty("benefits")]
        public List<Benefit> Benefits { get; set; }
        [JsonProperty("goals")]
        public List<Goal> Goals { get; set; }
    }
}