using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AoP.Patreon.Resources
{
    public class Benefit
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("benefit_type")]
        public string BenefitType { get; set; }
        [JsonProperty("rule_type")]
        public string RuleType { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("delivered_deliverables_count")]
        public int DeliveredDeliverablesCount { get; set; }
        [JsonProperty("not_delivered_deliverables_count")]
        public int NotDeliveredDeliverablesCount { get; set; }
        [JsonProperty("deliverables_due_today_cunt")]
        public int DeliverablesDueTodayCount { get; set; }
        [JsonProperty("next_deliverable_due_date")]
        public string NextDeliverabledDueDate { get; set; }
        [JsonProperty("tiers_count")]
        public int TiersCount { get; set; }
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }
    }
    public class BenefitMemberships
    {
        [JsonProperty("tiers")]
        public List<Tier> Tiers { get; set; }
        [JsonProperty("deliverables")]
        public List<Deliverable> Deliverables { get; set; }
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
    }
}
