using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.ProjectOxford.Common.Contract;
using N7Functions.Models;
using N7Functions.Utils;

namespace N7Functions
{
    public static class CardGenerator
    {
        [FunctionName("CardGenerator")]
        public static async Task Run([QueueTrigger("cardqueue", Connection = "")]CardInfoMessage cardInfo,[Blob("input-container/{BlobName}",FileAccess.Read)] byte[] image, [Blob("input-container/{BlobName}",FileAccess.Write)]Stream outputBlob, ILogger log, ExecutionContext context)
        {
            var faceDataArray = await CognitiveServices.RegonizeEmotionAsync(image, log);
            if (faceDataArray== null)
            {
                log.LogError("No results from Emotion API");
                return;
            }
            if (faceDataArray.Length == 0)
            {
                log.LogError("No face detected im image");
                return;
            }
            var faceData = faceDataArray[0];
            var testScores = new EmotionScores { Happiness = 1 };
            string cardPath = CognitiveServices.GetCardImageAndScores(faceDataArray[0].Scores, out double score, context.FunctionDirectory);
            ImageHelpersSkia.MergedCardImage(cardPath, image, outputBlob, cardInfo.PersonName, cardInfo.Tweet, score);
            log.LogInformation($"C# Queue trigger function processed: {cardInfo}");
        }
        [FunctionName("RequestImageProsessing")]
        [return: Queue("cardqueue")]
        public static CardInfoMessage RequestImageProcessing([HttpTrigger(AuthorizationLevel.Anonymous, new string[] { "POST" })]CardInfoMessage input, ILogger log)
        {
            return input;
        }

        [FunctionName("Settings")]
        public static SettingsMessage Settings([HttpTrigger(AuthorizationLevel.Anonymous, new string[] { "GET" })]string input, ILogger log)
        {
            string stage = (Environment.GetEnvironmentVariable("STAGE") == null) ? "LOCAL" : Environment.GetEnvironmentVariable("STAGE");
            return new SettingsMessage()
            {
                Stage = stage,
                SiteUrl = Environment.GetEnvironmentVariable("SITEURL"),
                StorageUrl= Environment.GetEnvironmentVariable("STORAGE_URL"),
                ContainerSAS= Environment.GetEnvironmentVariable("CONTAINER_SAS"),
                InputContainerName=Environment.GetEnvironmentVariable("input-container"),
                OutputContainerName=Environment.GetEnvironmentVariable("output-container")
            };
        }
        
    }
}
