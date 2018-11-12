using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wikia.Api;
using wikia.Models.Article;
using wikia.Models.Article.NewArticles;


namespace FunctionalWiki.Tools
{
    class Wikia
    {
        
        private string _domainurl = "masseffect.wikia.com";
        private IWikiArticle _sut;
        public async Task GetCharacters()
        {
            _sut = new WikiArticle(_domainurl);
            var Category = "Characters";
            
        }
        public Task<NewArticleResultSet> GetNewArticles()
        {
            _sut = new WikiArticle(_domainurl);
            _sut.NewArticles();
            return null;
        }
    }
}
