using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace N7Functions
{
    public static class UploadUrl
    {
        
        [FunctionName("UploadUrl")]
        public static async Task<object> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route =null)]HttpRequest req, ILogger log)
        {
            var filename = req.GetQueryParameterDictionary()
                .FirstOrDefault(q => string.Compare(q.Key, "filename", true) == 0)
                .Value;
            StorageAccount storageAccount = StorageAccount.NewFromConnectionString(
               Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING", EnvironmentVariableTarget.Process));
            var client = storageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(filename);
            var adHocSAS = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5),
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Create
            };
            var sasBlobToken = blob.GetSharedAccessSignature(adHocSAS);
            return new
            {
                url = blob.Uri + sasBlobToken
            };
        }
        
    }
}
