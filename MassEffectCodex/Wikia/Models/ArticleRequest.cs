
using System;
using System.Collections.Generic;
using System.Text;
namespace WikiaLib.Models
{
   public class ArticleRequest
    {
        public string Category { get; set; }
        public int LimitParam { get; set; }
        public int MinArticleQualityParam { get; set; }
    }
}
