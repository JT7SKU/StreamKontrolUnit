using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SkiaSharp;
using Microsoft.ProjectOxford.Common.Contract;


namespace N7Functions.Utils
{
   public class ImageHelpersSkia
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

        const int CardWidth = 819;
        const int CardHeight = 1150;
        #endregion

        #region Font info
        const string FontFamilyName = "Microsoft Sans Serif";
        const int NameFontSize = 38;
        const int TitleFontSize = 30;
        const short ScoreFontSize = 55;
        #endregion

        public static void MergedCardImage(string cardPath, byte[] imageBytes, Stream outputStream, string personName, string personTweet, double score)
        {
            using (MemoryStream faceImageStream = new MemoryStream(imageBytes))
            {
                using (var surface = SKSurface.Create(width: CardWidth, height: CardHeight, colorType: SKImageInfo.PlatformColorType, alphaType: SKAlphaType.Premul))
                {
                    SKCanvas canvas = surface.Canvas;

                    canvas.DrawColor(SKColors.White);

                    using (var fileStream = File.OpenRead(cardPath))
                    using (var stream = new SKManagedStream(fileStream))
                    using (var cardBack = SKBitmap.Decode(stream))
                    using (var face = SKBitmap.Decode(imageBytes))
                    using (var paint = new SKPaint())
                    {
                        canvas.DrawBitmap(cardBack, SKRect.Create(0, 0, CardWidth, CardHeight), paint);
                        canvas.DrawBitmap(face, SKRect.Create(TopLeftFaceX, TopLeftFaceY, FaceRect, FaceRect));

                        RenderText(canvas, NameFontSize, NameTextX, NameTextY, NameWidth, personName);
                        RenderText(canvas, TitleFontSize, NameTextX, NameTextY, NameWidth, personTweet);
                        RenderScore(canvas, ScoreX, ScoreY, ScoreWidth, score.ToString());

                        canvas.Flush();
                        using (var jpegImage = surface.Snapshot().Encode(SKEncodedImageFormat.Jpeg, 80))
                        {
                            jpegImage.SaveTo(outputStream);
                        }
                    }
                }

            }
        }

        private static void RenderScore(SKCanvas canvas, int posX, int posY, int width, string score)
        {
            var font = SKTypeface.FromFamilyName(FontFamilyName, SKTypefaceStyle.Bold);
            var brush = CreateBrush(font, ScoreFontSize);
            var textWidth = brush.MeasureText(score);
            canvas.DrawText(score, posX + width - textWidth, posY, brush);
        }

        private static SKPaint CreateBrush(SKTypeface font, int fontSize)
        {
            return new SKPaint
            {
                Typeface = font,
                TextSize = fontSize,
                IsAntialias = true,
                Color = new SKColor(0, 0, 0)
            };
        }

        private static void RenderText(SKCanvas canvas, int fontSize, int posX, int posY, int width, string text)
        {
            var font = SKTypeface.FromFamilyName(FontFamilyName, SKTypefaceStyle.Bold);

            SKPaint brush = null;
            float textWidth;
            do
            {
                brush = CreateBrush(font, fontSize);
                textWidth = brush.MeasureText(text);
                fontSize--;
            }
            while (textWidth > width);

            canvas.DrawText(text, posX, posY, brush);
        }
    }
}
