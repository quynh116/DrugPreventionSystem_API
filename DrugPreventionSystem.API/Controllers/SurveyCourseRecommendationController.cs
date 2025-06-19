using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Models.Responses.SurveyCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyCourseRecommendationController : BaseApiController
    {
        private readonly ISurveyCourseRecommendationService _surveyCourseRecommendationService;

        public SurveyCourseRecommendationController(ISurveyCourseRecommendationService surveyCourseRecommendationService)
        {
            _surveyCourseRecommendationService = surveyCourseRecommendationService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<SurveyCourseRecommendationResponse>>>> GetAllRecommendations()
        {
            var result = await _surveyCourseRecommendationService.GetAllSurveyCourseRecommendationAsync();
            return HandleResult(result);
        }

        [HttpGet("{recommendationId}")]
        public async Task<ActionResult<Result<SurveyCourseRecommendationResponse>>> GetRecommendationById(Guid recommendationId)
        {
            var result = await _surveyCourseRecommendationService.GetRecommendationByIdAsync(recommendationId);
            return HandleResult(result);
        }
        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<Result<IEnumerable<SurveyCourseRecommendationResponse>>>> GetRecommendationsByCourseId(Guid courseId)
        {
            var result = await _surveyCourseRecommendationService.GetRecommendationsByCourseIdAsync(courseId);
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<Result<SurveyCourseRecommendationResponse>>> AddRecommendation([FromBody] SurveyCourseRecommendationCreateRequest request)
        {
            var result = await _surveyCourseRecommendationService.AddRecommendationAsync(request);
            return HandleResult(result);
        }
        [HttpPut("{recommendationId}")]
        public async Task<ActionResult<Result<SurveyCourseRecommendationResponse>>> UpdateRecommendation(Guid recommendationId, [FromBody] SurveyCourseRecommendationUpdateRequest request)
        {
            var result = await _surveyCourseRecommendationService.UpdateRecommendationAsync(recommendationId, request);
            return HandleResult(result);
        }
        [HttpDelete("{recommendationId}")]
        public async Task<ActionResult<Result<bool>>> DeleteRecommendation(Guid recommendationId)
        {
            var result = await _surveyCourseRecommendationService.DeleteRecommendationAsync(recommendationId);
            return HandleResult(result);
        }
    }
}