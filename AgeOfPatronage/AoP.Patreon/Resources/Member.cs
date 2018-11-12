using Newtonsoft.Json;
using System.Collections.Generic;

namespace AoP.Patreon.Resources
{
    public class Member
    {
        [JsonProperty("patron_status")]
        public string PatronStatus { get; set; }
        [JsonProperty("is_follower")]
        public bool IsFollower { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("pledge_relationship_start")]
        public string PledgeRelationshipStart { get; set; }
        [JsonProperty("lifetime_relationship_start")]
        public int LifeTimeSupportCents { get; set; }
        [JsonProperty("currently_entitled_amount_cents")]
        public int CurrentlyEntitledAmountCents { get; set; }
        [JsonProperty("last_charge_date")]
        public string LastChargeDate { get; set; }
        [JsonProperty("last_charge_status")]
        public string LastChargeStatus { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("will_pay_amount_cents")]
        public int WillPayAmountCents { get; set; }
    }
    public class MemberRelationShips
    {
        [JsonProperty("address")]
        public Address Address { get; set; }
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
        [JsonProperty("currently_entitled_tiers")]
        public List<Tier> CurrentlyEntitledTiers { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
    }
}