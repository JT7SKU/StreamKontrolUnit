using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AoP.Patreon.Resources
{
    public class Address
    {
        [JsonProperty("adressee")]
        public string Adressee { get; set; }
        [JsonProperty("line_1")]
        public string Line1 { get; set; }
        [JsonProperty("line_2")]
        public string Line2 { get; set; }
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string State { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("confirmed")]
        public bool Confirmed { get; set; }
        [JsonProperty("confirmed_at")]
        public DateTime ConfirmedAt { get; set; }
    }
    public class AddressRelationship
    {
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("campaigns")]
        public List<Campaign> Campaigns { get; set; }
    }
}