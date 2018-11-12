using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace AoP.Patreon.Resources
{
    public class OAuthClient
    {
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }
        [JsonProperty("domain")]
        public string Domain { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        [JsonProperty("privacy_policy_url")]
        public string PrivacyPolicyUrl { get; set; }
        [JsonProperty("tos_url")]
        public string TosUrl { get; set; }
        [JsonProperty("redirect_urls")]
        public string RedirectUrls { get; set; }
        [JsonProperty("default_scopes")]
        public string DefaultScopes { get; set; }
    }
    public class OAuthClientRelationships
    {
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
    }
}
