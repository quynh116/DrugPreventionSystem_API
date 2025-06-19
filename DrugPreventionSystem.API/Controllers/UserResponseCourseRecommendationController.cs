using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserResponseCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
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
        public async Task<ActionResult<Result<UserResponseCourseRecommendationResponse>>> AddUserResponseAsync([FromBody] UserResponseCourseRecommendationCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserResponseCourseRecommendationResponse>.Invalid("Invalid request data.", errors));
            }
            var result = await _userResponseCourseRecommendationService.AddUserResponseAsync(request);
            return HandleResult(result);
        }

        [HttpDelete("{userRecId}")]
        public async Task<ActionResult<Result<UserResponseCourseRecommendationResponse>>> DeleteUserResponseAsync(Guid userRecId)
        {
            var rec = await _userResponseCourseRecommendationService.GetRecommendationsByUserRecIdAsync(userRecId);
            if (rec == null)
            {
                return NoContent();
            }
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

        [HttpGet("recommended-courses/response/{responseId}")]
        public async Task<ActionResult<Result<IEnumerable<CourseResponse>>>> GetRecommendedCoursesByResponseId(Guid responseId)
        {
            
            var result = await _userResponseCourseRecommendationService.GetRecommendedCoursesByResponseIdAsync(responseId);

            
            return HandleResult(result);
        }

        [HttpPut("{userRecId}")]
        public async Task<ActionResult<Result<UserResponseCourseRecommendationResponse>>> UpdateUserResponseAsync(Guid userRecId, [FromBody] UserResponseCourseRecommendationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserResponseCourseRecommendationResponse>.Invalid("Invalid request data.", errors));
            }
            var result = await _userResponseCourseRecommendationService.UpdateUserResponseAsync(userRecId, request);
            return HandleResult(result);
        }
    }
}
