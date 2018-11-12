using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AoP.Patreon.Resources
{
    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("is_email_verified")]
        public bool IsEmailVerified { get; set; }
        [JsonProperty("vanity")]
        public string Vanity { get; set; }
        [JsonProperty("about")]
        public string About { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
        [JsonProperty("can_see_nsfw")]
        public bool CanSeeNSFW { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("like_count")]
        public int LikeCount { get; set; }
        [JsonProperty("hide_pledges")]
        public bool HidePledges { get; set; }
        [JsonProperty("social_connections")]
        public object SocialConnections { get; set; }
    }
    public class UserRelationships
    {
        [JsonProperty("memberships")]
        public List<Member> Memberships { get; set; }
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
    }
}
