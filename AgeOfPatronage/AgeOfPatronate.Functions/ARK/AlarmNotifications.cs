using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AgeOfPatronage.Functions.ARK
{
    public static class AlarmNotifications
    {
        [FunctionName("AlarmNotifications")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var Line1 = req.Query["webapikey"];
            var Line2 = req.Query["url"];
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;


            return name != null
                ? (ActionResult)new OkObjectResult($"{name} write AlarmPostCredentials.txt file these values {Line1} and {Line2}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
