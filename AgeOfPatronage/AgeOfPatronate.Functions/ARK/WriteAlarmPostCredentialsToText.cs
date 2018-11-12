using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AgeOfPatronage.Functions.ARK
{
    public static class WriteAlarmPostCredentialsToText
    {
        [FunctionName("WriteAlarmPostCredentialsToText")]
        public static void Run([BlobTrigger("ShooterGame/Saved/{name}.txt", Connection = "")]Stream myBlob,string name, string webapikey,string url, ILogger log)
        {
            // e.q, /shooterGame/Saved/AlarmPostCredentials.txt
            // 01234abcdef01234abcdef
            // https://mywebsite.com/index.php?myowncustom=thing&another=thing


            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n {webapikey} \n {url} \n Size: {myBlob.Length} Bytes");

        }
    }
}
