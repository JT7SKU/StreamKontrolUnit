using System;
using System.Collections.Generic;
using System.Text;

namespace WikiaLib.Models
{
    public class NewArticleResultSet
    {
        public NewArticle[] Items { get; set; }
        public string Basepath { get; set; }
    }
}
