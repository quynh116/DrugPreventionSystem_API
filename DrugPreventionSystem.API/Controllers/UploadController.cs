using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly ICertificateService _certificateService;

        public UploadController(IPhotoService photoService, ICertificateService certificateService)
        {
            _photoService = photoService;
            _certificateService = certificateService;
        }

        [HttpGet("generate-test-certificate")]
        public async Task<IActionResult> GenerateTestCertificate()
        {
            try
            {
                var testData = new CertificateData
                {
                    UserName = "Nguyễn Văn An",
                    CourseTitle = "Khóa học phòng chống tệ nạn xã hội",
                    CompletionDate = DateTime.Now,
                    DurationWeeks = "8 tuần",
                    InstructorName = "Nguyễn Minh Tuấn"
                };

                var certificateUrl = await _certificateService.GenerateCertificateWithTemplateAsync(testData);

                return Ok(new
                {
                    success = true,
                    message = "Certificate generated successfully",
                    certificateUrl = certificateUrl
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var url = await _photoService.UploadImageAsync(file);
            if (url == null)
                return BadRequest("Upload failed");

            return Ok(new { Url = url });
        }

        [HttpPost("upload-video")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            var url = await _photoService.UploadVideoAsync(file);
            if (url == null)
                return BadRequest("Upload failed");

            return Ok(new { Url = url });
        }

        [HttpPost("upload-document")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            var url = await _photoService.UploadDocumentAsync(file);
            if (url == null)
                return BadRequest("Upload failed");

            return Ok(new { Url = url });
        }
    }
}
