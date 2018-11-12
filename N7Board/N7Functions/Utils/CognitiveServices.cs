using Microsoft.ProjectOxford.Emotion;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.ProjectOxford.Common.Contract;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace N7Functions.Utils
{
    public class CognitiveServices
    {
        #region Face and Emotion
        private const string AssetsFolderLocation = "assets";
        

        public static async Task<Emotion[]> RegonizeEmotionAsync(byte[] image, ILogger Log)
        {
            try
            {
                var emotionServiceClient = new EmotionServiceClient(Environment.GetEnvironmentVariable("EMOTIONAPIKEYNAME"));
                using (MemoryStream faceimageStream = new MemoryStream(image))
                {
                    return await emotionServiceClient.RecognizeAsync(faceimageStream);
                }
            }
            catch (Exception e)
            {
                Log.LogError($"Error when processing image: {e.Message}");
                return null;
            }
        }
        public static string GetCardImageAndScores(EmotionScores scores, out double score, string functionDirectory)
        {
            NormalizeScores(scores);
            var cardBack = "neutral.png";
            score = scores.Neutral;
            const int angerBoost = 2, happyBoost = 4;

            if (scores.Surprise > 10)
            {
                cardBack = "surprised.png";
                score = scores.Surprise;
            }
            else if (scores.Anger > 10)
            {
                cardBack = "angry.png";
                score = scores.Anger * angerBoost;
            }
            else if (scores.Happiness > 50)
            {
                cardBack = "happy.png";
                score = scores.Happiness * happyBoost;
            }
            var path = Path.Combine(functionDirectory, "..\\", AssetsFolderLocation, cardBack);

            return Path.GetFullPath(path);
        }

        public static float RoundScore(float score) => (float)Math.Round((decimal)(score * 100), 0);
        public static void NormalizeScores(EmotionScores scores)
        {
            scores.Anger = RoundScore(scores.Anger);
            scores.Happiness = RoundScore(scores.Happiness);
            scores.Neutral = RoundScore(scores.Neutral);
            scores.Sadness = RoundScore(scores.Sadness);
            scores.Surprise = RoundScore(scores.Surprise);

        }
        #endregion
        #region Vision
        // ANalyze
        private static string subscriptionKey="<subsciptionkey>";
        const string uriBase =
            "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/analyze";
        /// <summary>
        /// Gets the analysis of the specified image file by using
        /// the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file to analyze.</param>
        static async Task MakeAnalysisRequest(string imageFilePath)
        {
            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", subscriptionKey);

                // Request parameters. A third optional parameter is "details".
                // The Analyze Image method returns information about the following
                // visual features:
                // Categories:  categorizes image content according to a
                //              taxonomy defined in documentation.
                // Description: describes the image content with a complete
                //              sentence in supported languages.
                // Color:       determines the accent color, dominant color, 
                //              and whether an image is black & white.
                string requestParameters =
                    "visualFeatures=Description&amp;language=en";

                // Assemble the URI for the REST API method.
                string uri = uriBase + "?" + requestParameters;

                HttpResponseMessage response;

                // Read the contents of the specified local image
                // into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                // Add the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json"
                    // and "multipart/form-data".
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    // Asynchronously call the REST API method.
                    response = await client.PostAsync(uri, content);
                }

                // Asynchronously get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n\n{0}\n",
                    JToken.Parse(contentString).ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
        #endregion

    }
}
