using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassEffectCodex.Models
{
    public partial class Article
    {
        [JsonProperty("article")]
        public ArticleElement[] ArticleArticle { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("basepath")]
        public string Basepath { get; set; }
    }

    public partial class ArticleElement
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("ns")]
        public string Ns { get; set; }
    }
}
