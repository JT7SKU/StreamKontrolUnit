using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wikia.Api;
using FunctionalWiki;
using WikiaLib.Models;
using Microsoft.Extensions.Logging;
using Windows.Web.Http;

namespace MassEffectCodex.Services
{
    class WikiaService
    {
        Uri functionurl = new Uri("http://localhost/api/");
        //IWikiArticle Articles = new WikiArticle(domainUrl);
        HttpClient wikiaClient = new HttpClient();
        
        public Task GetGames()
        {

            return Task.CompletedTask;
        }
        
        public Task GetMissions(string game)
        {
            return Task.CompletedTask;
        }
        public async Task GetArticles()
        {
            ArticleRequest articleRequest = new ArticleRequest();
            articleRequest.Category = "";
            articleRequest.LimitParam = 20;
            articleRequest.MinArticleQualityParam = 10;
            using(var httpclient = new HttpClient())
            {
                try
                {

                    HttpResponseMessage httpresponse = await wikiaClient.GetAsync(functionurl);
                    //var result = await wikiaClient.SendRequestAsync(articleRequest);
                    httpresponse.EnsureSuccessStatusCode();
                    var httpresponsebody = await httpresponse.Content.ReadAsInputStreamAsync();
                    //var result = await WikiaFuncs.GetNewArticles(articleRequest,null);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
            //var result = await WikiaFuncs.GetNewArticles(articleRequest,null);
            //return result;
            
        }
    }
}
