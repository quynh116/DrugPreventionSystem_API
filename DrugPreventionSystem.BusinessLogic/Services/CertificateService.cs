using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IPhotoService _photoService;
        private readonly string _templateUrl = "https://res.cloudinary.com/dvkqdbaue/image/upload/v1752323084/uploads/Copilot_20250712_191953_dgd0cm.png";

        public CertificateService(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        

        public async Task<string> GenerateCertificateWithTemplateAsync(CertificateData data)
        {
            try
            {
                using var httpClient = new HttpClient();
                var templateBytes = await httpClient.GetByteArrayAsync(_templateUrl);

                using var templateStream = new MemoryStream(templateBytes);
                using var templateBitmap = SKBitmap.Decode(templateStream);

                using var surface = SKSurface.Create(new SKImageInfo(templateBitmap.Width, templateBitmap.Height));
                var canvas = surface.Canvas;

                canvas.Clear(SKColors.White);
                canvas.DrawBitmap(templateBitmap, 0, 0);

                await DrawTextOnTemplate(canvas, data, templateBitmap.Width, templateBitmap.Height);

                using var image = surface.Snapshot();
                using var encoded = image.Encode(SKEncodedImageFormat.Png, 100);
                var imageBytes = encoded.ToArray();

                var fileName = $"certificate_{Guid.NewGuid()}.png";
                var formFile = CreateFormFile(imageBytes, fileName, "image/png");

                string certificateUrl = await _photoService.UploadImageAsync(formFile);

                return certificateUrl;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating certificate: {ex.Message}", ex);
            }
        }

        private IFormFile CreateFormFile(byte[] fileBytes, string fileName, string contentType)
        {
            var stream = new MemoryStream(fileBytes);
            return new FormFileFromStream(stream, fileName, contentType);
        }

        private async Task DrawTextOnTemplate(SKCanvas canvas, CertificateData data, int width, int height)
        {
            var fontFamily = "Arial";

            using var namePaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = CalculateTextSize(data.UserName, width * 0.6f, 48),
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName(fontFamily, SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            };

            float nameY = height * 0.38f;
            DrawCenteredText(canvas, data.UserName.ToUpper(), namePaint, width / 2, nameY);

            using var coursePaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 32, 
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName(fontFamily, SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            };

            float courseY = height * 0.52f;
            DrawWrappedCenteredText(canvas, data.CourseTitle, coursePaint, width / 2, courseY, width * 0.8f);

            using var detailPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 24, 
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName(fontFamily, SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            };

            float leftColumnCenterX = width * 0.25f;   
            float rightColumnCenterX = width * 0.65f;  
            float detailStartY = height * 0.62f;       

            DrawCenteredText(canvas, data.CompletionDate.ToString("dd/MM/yyyy"), detailPaint, leftColumnCenterX, detailStartY);
            DrawCenteredText(canvas, data.DurationWeeks, detailPaint, rightColumnCenterX, detailStartY);

            float instructorCenterX = width * 0.32f;        
            float instructorY = detailStartY + 105;      
            DrawCenteredText(canvas, data.InstructorName, detailPaint, instructorCenterX, instructorY);
        }

        private void DrawCenteredText(SKCanvas canvas, string text, SKPaint paint, float centerX, float y)
        {
            var bounds = new SKRect();
            paint.MeasureText(text, ref bounds);
            float x = centerX - bounds.Width / 2;
            canvas.DrawText(text, x, y, paint);
        }

        private void DrawWrappedCenteredText(SKCanvas canvas, string text, SKPaint paint, float centerX, float y, float maxWidth)
        {
            var bounds = new SKRect();
            paint.MeasureText(text, ref bounds);

            if (bounds.Width <= maxWidth)
            {
                DrawCenteredText(canvas, text, paint, centerX, y);
                return;
            }

            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = "";

            foreach (var word in words)
            {
                var testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                paint.MeasureText(testLine, ref bounds);

                if (bounds.Width <= maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        lines.Add(currentLine);
                        currentLine = word;
                    }
                    else
                    {
                        lines.Add(word);
                    }
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                lines.Add(currentLine);
            }

            if (lines.Count > 2)
            {
                var secondLine = lines[1];
                var words2 = secondLine.Split(' ').ToList();

                while (words2.Count > 0)
                {
                    var testLine = string.Join(" ", words2) + "...";
                    paint.MeasureText(testLine, ref bounds);

                    if (bounds.Width <= maxWidth)
                    {
                        lines[1] = testLine;
                        break;
                    }
                    words2.RemoveAt(words2.Count - 1);
                }

                lines = lines.Take(2).ToList();
            }

            float lineHeight = paint.TextSize * 1.2f;
            float startY = y - (lines.Count - 1) * lineHeight / 2;

            for (int i = 0; i < lines.Count; i++)
            {
                DrawCenteredText(canvas, lines[i], paint, centerX, startY + i * lineHeight);
            }
        }

        private void DrawLeftAlignedText(SKCanvas canvas, string text, SKPaint paint, float x, float y)
        {
            canvas.DrawText(text, x, y, paint);
        }

        private float CalculateTextSize(string text, float maxWidth, float maxSize)
        {
            using var paint = new SKPaint
            {
                Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright),
                TextSize = maxSize
            };

            var bounds = new SKRect();
            paint.MeasureText(text, ref bounds);

            if (bounds.Width <= maxWidth)
                return maxSize;

            float ratio = maxWidth / bounds.Width;
            return Math.Max(maxSize * ratio, 16); 
        }
    }

    public class FormFileFromStream : IFormFile
    {
        private readonly Stream _stream;
        private readonly string _fileName;
        private readonly string _contentType;

        public FormFileFromStream(Stream stream, string fileName, string contentType)
        {
            _stream = stream;
            _fileName = fileName;
            _contentType = contentType;
        }

        public string ContentType => _contentType;
        public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{_fileName}\"";
        public IHeaderDictionary Headers { get; set; } = new HeaderDictionary();
        public long Length => _stream.Length;
        public string Name => "file";
        public string FileName => _fileName;

        public void CopyTo(Stream target)
        {
            _stream.Position = 0;
            _stream.CopyTo(target);
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            _stream.Position = 0;
            await _stream.CopyToAsync(target, cancellationToken);
        }

        public Stream OpenReadStream()
        {
            _stream.Position = 0;
            return _stream;
        }
    }
}