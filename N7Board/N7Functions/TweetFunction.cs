using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace N7Functions
{
    public static class TweetFunction
    {
        [FunctionName("TweetSentiment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string category = "GREEN";
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(string.Format("The sentiment score received is '{0}'.", requestBody));
            double score = Convert.ToDouble(requestBody);
            if (score < .3)
            {
                category = "RED";
            }
            else if (score < .6)
            {
                category = "YELLOW";
            }
            return requestBody != null
                ? (ActionResult)new OkObjectResult(category)
                : new BadRequestObjectResult("Please pass a value on the query string or in the request body");
        }
    }
}
