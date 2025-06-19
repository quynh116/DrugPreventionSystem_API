using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserResponseCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserResponseCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserResponseCourseRecommendationController : BaseApiController
    {
        private readonly IUserResponseCourseRecommendationService _userResponseCourseRecommendationService;

        public UserResponseCourseRecommendationController(IUserResponseCourseRecommendationService userResponseCourseRecommendationService)
        {
            _userResponseCourseRecommendationService = userResponseCourseRecommendationService;
        }
        [HttpPost]
        public async Task<ActionResult<Result<UserResponseCourseRecommendationResponse>>> AddUserResponseAsync(UserResponseCourseRecommendationCreateRequest request)
        {
            var result = await _userResponseCourseRecommendationService.AddUserResponseAsync(request);
            return HandleResult(result);
        }

        [HttpDelete("{userRecId}")]
        public async Task<ActionResult<Result<UserResponseCourseRecommendationResponse>>> DeleteUserResponseAsync(Guid userRecId)
        {
            var result = await _userResponseCourseRecommendationService.DeleteUserResponseAsync(userRecId);
            return HandleResult(result);
        }
        [HttpGet("{userRecId}")]
        public async Task<ActionResult<Result<UserResponseCourseRecommendationResponse>>> GetRecommendationsByUserRecIdAsync(Guid userRecId)
        {
            var result = await _userResponseCourseRecommendationService.GetRecommendationsByUserRecIdAsync(userRecId);
            return HandleResult(result);
        }
        [HttpGet("response/{responseId}")]
        public async Task<ActionResult<Result<IEnumerable<UserResponseCourseRecommendationResponse>>>> GetUsersByResponseIdAsync(Guid responseId)
        {
            var result = await _userResponseCourseRecommendationService.GetUsersByResponseIdAsync(responseId);
            return HandleResult(result);
        }
    }
}
