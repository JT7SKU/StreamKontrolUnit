using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp.Formats;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Gif;
using Microsoft.WindowsAzure.Storage;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Http;

namespace N7Functions
{
    public static class ResizeImages
    {
        static HttpClient httpClient = new HttpClient();
        static HttpRequest req;
        [FunctionName("ResizeImages")]
        public static async Task<object> Run([BlobTrigger("n7/{name}",Connection ="storageconnectionstring")]Stream myBlob, string name, Stream thumbnail, ILogger log)
        {
            //Add Image Resize thingy here
            req.PathBase= Environment.GetEnvironmentVariable("COMP_VISION_URL", EnvironmentVariableTarget.Process);
            req.Path = " / analyze ? visualFeatures = Description & amp; language = en";
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    Environment.GetEnvironmentVariable("COMP_VISION_URL", EnvironmentVariableTarget.Process) +
                    "/analyze?visualFeatures=Description&amp;language=en"),
                Method = HttpMethod.Post,
                Content = new StreamContent(myBlob)
            };
           
            request.Headers.Add(
                "Ocp-Apim-Subscription-Key",
                System.Environment.GetEnvironmentVariable("COMP_VISION_KEY", EnvironmentVariableTarget.Process));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var response = await httpClient.SendAsync(request);
            dynamic result = await response.Content.ReadAsAsync<object>();

            return new
            {
                id = name,
                imgPath = "/images/" + name,
                thumbnailPath = "/thumbnails/" + name,
                description = result.description

            };
        }
        [FunctionName("TumbnailCreation")]
        public static async Task TumbnailCreate([EventGridTrigger]EventGridEvent eventGridEvent, [Blob("{data.url}", FileAccess.Read)]Stream Input, ILogger logger)
        {
            try
            {
                var createEvent = ((JObject)eventGridEvent.Data).ToObject<StorageBlobCreatedEventData>();
                var extension = Path.GetExtension(createEvent.Url);
                var encoder = GetEncoder(extension);
                if (encoder != null)
                {
                    var thumbnailWidth = Convert.ToInt32(Environment.GetEnvironmentVariable("[THUMBNAIL_WIDTH]"));
                    var thumbnailContainerName = Environment.GetEnvironmentVariable("[THUMBNAIL_CONTAINER_NAME]");
                    var storageAccount = CloudStorageAccount.Parse("BLOB_STORAGE_CONNECTION_STRING");
                    var blobClient = storageAccount.CreateCloudBlobClient();
                    var container = blobClient.GetContainerReference(thumbnailContainerName);
                    var blobName = GetBlobNameFromUrl(createEvent.Url);
                    var blockBlob = container.GetBlockBlobReference(blobName);

                    using (var output = new MemoryStream())
                    using (Image<Rgba32> image = Image.Load(Input))
                    {
                        var divisor = image.Width / thumbnailWidth;
                        var height = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));
                        image.Mutate(x => x.Resize(thumbnailWidth, height));
                        image.Save(output, encoder);
                        output.Position = 0;
                        await blockBlob.UploadFromStreamAsync(output);
                    }
                }
                else
                {
                    logger.LogInformation($"No encoder support for: {createEvent.Url}");
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                throw;
            }
        }

        private static IImageEncoder GetEncoder(string extension)
        {
            IImageEncoder encoder = null;
            extension = extension.Replace(".", "");
            var isSupported = Regex.IsMatch(extension,"gif|png|jp?g", RegexOptions.IgnoreCase);
            if (isSupported)
            {
                switch (extension)
                {
                    case "png":
                        encoder = new PngEncoder();
                        break;
                    case "jpg":
                        encoder = new JpegEncoder();
                        break;
                    case "jpeg":
                        encoder = new JpegEncoder();
                        break;
                    case "gif":
                        encoder = new GifEncoder();
                        break;
                    default:
                        break;
                }
            }
            return encoder;
        }

        private static string GetBlobNameFromUrl(string blobUrl)
        {
            var url = new Uri(blobUrl);
            var cloudBlob = new CloudBlob(url);
            return cloudBlob.Name;
        }
    }
}
