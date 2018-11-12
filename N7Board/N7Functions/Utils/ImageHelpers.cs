using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Primitives;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace N7Functions.Utils
{
    public class ImageHelpers
    {
        #region Pixel locations
        const int TopLeftFaceX = 85;
        const int TopLeftFaceY = 187;
        const int FaceRect = 648;
        const int NameTextX = 56;
        const int NameTextY = 60;
        const int TitleTextX = 108;
        const int NameWidth = 430;
        const int ScoreX = 654;
        const int ScoreY = 70;
        const int ScoreWidth = 117;
        #endregion

        #region Font info
        const string FontFamilyName = "Microsoft Sans Serif";
        const int NameFontSize = 38;
        const int TitleFontSize = 30;
        const short ScoreFontSize = 55;
        #endregion

        public static void MergedCardImage(string cardPath, byte[] imageBytes, Stream outputStream, string personName, string personTitle, double score)
        {
            var extension = Path.GetExtension(cardPath);
            var encoder = GetEncoder(extension);
            using (MemoryStream faceImageStream = new MemoryStream(imageBytes))
            {
               
                using (var  faceImage = Image.Load(cardPath))
                {
                    using (Image<Rgba32> image = Image.Load(imageBytes))
                    {
                      //image(faceImage, TopLeftFaceX, TopLeftFaceY, FaceRect, FaceRect);

                        //RenderText(g, NameFontSize, NameTextX, NameTextY, NameWidth, personName);
                        //RenderText(g, TitleFontSize, NameTextX + 4, TitleTextX, NameWidth, personTitle); // second line seems to need some left padding



                        //RenderScore(g, ScoreX, ScoreY, ScoreWidth, score.ToString());

                    }
                } 
            } 
        }

        private static void RenderScore(IImageProcessingContext<Rgba32> g, int scoreX, int scoreY, int scoreWidth, string v)
        {

        }

        private static void RenderText(IImageProcessingContext<Rgba32> g, int nameFontSize, int nameTextX, int nameTextY, int nameWidth, string personName)
        {
            throw new NotImplementedException();
        }

        private static IImageEncoder GetEncoder(string extension)
        {
            IImageEncoder encoder = null;
            extension = extension.Replace(".", "");
            var isSupported = Regex.IsMatch(extension, "gif|png|jp?g", RegexOptions.IgnoreCase);
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
    }
}
