using System;
using System.Collections.Generic;
using System.Text;

namespace WikiaLib.Models
{
    class NewArticleRequestParameters
    {
        public HashSet<string> Namespaces { get; set; } = new HashSet<string>();
        public int Limit { get; set; } = 20; //MaXLimit is 100
        public int MinArticleQuality { get; set; } = 10; // Ranges Minimum from 0 to 99
    }
}
