using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WikiaLib.Models
{
    public class NewArticle
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("ns")]
        public string Ns { get; set; }
        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("original_dimensions")]
        public OriginalDimensions OriginalDimensions { get; set; }
        [JsonProperty("abstract")]
        public string Abstract { get; set; }

        [JsonProperty("creator")]
        public Creator Creator { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }
    }
   

}
