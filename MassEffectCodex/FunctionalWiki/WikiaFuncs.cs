
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using wikia.Api;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using wikia.Models.Article;
using wikia.Models.Article.NewArticles;
using wikia.Models.Article.AlphabeticalList;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using WikiaLib.Models;

namespace FunctionalWiki
{
    public static class WikiaFuncs
    {
        private static JArray Articles;

        [FunctionName("GetNewArticles")]
        public static async Task<IActionResult> GetNewArticles([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Articles/New")]ArticleRequest req,  ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var limit = 20;
            var MinArticleQuality = 10;
            string domainUrl = "http://masseffect.wikia.com/";
            IWikiArticle _articles = new WikiArticle(domainUrl);
            List<WikiaLib.Models.NewArticle> Articles = new List<WikiaLib.Models.NewArticle>();
            wikia.Models.Article.NewArticles.NewArticleRequestParameters requestParameters = new wikia.Models.Article.NewArticles.NewArticleRequestParameters( ) ;
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["limit"] = limit.ToString(),
                ["minArticleQuality"] = MinArticleQuality.ToString()
            };
            NewArticleRequestParameters articleRequestParameters = new NewArticleRequestParameters();
            articleRequestParameters.Limit = req.LimitParam;
            articleRequestParameters.MinArticleQuality = req.MinArticleQualityParam;

            var result =  _articles.NewArticles();
            var lenght = result.Result.Items.LongLength;
            
            var wikibasepath = result.Result.Basepath.ToString();
            var items = result.Result.Items;
            IEnumerable<wikia.Models.Article.NewArticles.NewArticle> newArticles = items;
            List<wikia.Models.Article.NewArticles.NewArticle> articles = new List<wikia.Models.Article.NewArticles.NewArticle>();
           
            foreach( var article in items)
            {
                articles.Add(article);
                //await cosmosDBCollection.AddAsync(article);
            }

            //string name = req.Query["name"];
            //var results = await _articles.NewArticles(requestParameters);
            //string requestBody = new StreamReader(req.Body).ReadToEnd();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;



            return new OkResult();
        }
        [FunctionName("GetArticles")]
        public static async Task<HttpResponseMessage> GetArticles([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Articles/List")]HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string domainUrl = "http://masseffect.wikia.com/";
            IWikiArticle articles = new WikiArticle(domainUrl);
            var category = "Mass Effect: Andromeda";
            var result = articles.AlphabeticalList(category);
            var MEAList = new List<UnexpandedArticle>();
            foreach (var article in result.Result.Items)
            {
                MEAList.Add(article);
                //await cosmosDBCollection.AddAsync(article);
            }
            //string name = req.Query["name"];

            //string requestBody = new StreamReader(req.Body).ReadToEnd();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            return MEAList != null
                ? req.CreateResponse(HttpStatusCode.OK,$"Hello, {MEAList}")
                : req.CreateResponse(HttpStatusCode.BadRequest,"Please pass a name on the query string or in the request body");
        }
        
    }
}
