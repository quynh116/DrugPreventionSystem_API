using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyAnswer;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyAnswer;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSurveyAnswerController : BaseApiController
    {
        private readonly IUserSurveyAnswerService _userSurveyAnswerService;

        public UserSurveyAnswerController(IUserSurveyAnswerService userSurveyAnswerService)
        {
            _userSurveyAnswerService = userSurveyAnswerService;
        }

        [HttpPost]
        public async Task<ActionResult<Result<UserSurveyAnswerResponse>>> AddUserSurveyAnswerAsync([FromBody] UserSurveyAnswerCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserSurveyAnswerResponse>.Invalid("Invalid request data.", errors));
            }
            var result = await _userSurveyAnswerService.AddUserSurveyAnswerAsync(request);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserSurveyAnswerResponse>>> GetUserSurveyAnswerByIdAsync(Guid id)
        {
            var result = await _userSurveyAnswerService.GetUserSurveyAnswerByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Result<IEnumerable<UserSurveyAnswerResponse>>>> GetUserSurveyAnswersByUserIdAsync(Guid userId)
        {
            var result = await _userSurveyAnswerService.GetUserSurveyAnswersByUserIdAsync(userId);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<UserSurveyAnswerResponse>>> UpdateUserSurveyAnswerAsync(Guid id, [FromBody] UserSurveyAnswerUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserSurveyAnswerResponse>.Invalid("Invalid update data.", errors));
            }
            var result = await _userSurveyAnswerService.UpdateUserSurveyAnswerAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteUserSurveyAnswerAsync(Guid id)
        {
            var result = await _userSurveyAnswerService.DeleteUserSurveyAnswerAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
