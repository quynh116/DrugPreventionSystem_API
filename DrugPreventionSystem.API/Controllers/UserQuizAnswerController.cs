using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserQuizAnswer;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserQuizAnswer;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuizAnswerController : BaseApiController
    {
        private readonly IUserQuizAnswerService _userQuizAnswerService;

        public UserQuizAnswerController(IUserQuizAnswerService userQuizAnswerService)
        {
            _userQuizAnswerService = userQuizAnswerService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserQuizAnswerResponse>>>> GetUserQuizAnswers()
        {
            var result = await _userQuizAnswerService.GetUserQuizAnswersAsync();
            return HandleResult(result);
        }
        [HttpGet("{userQuizAnswerId}")]
        public async Task<ActionResult<Result<UserQuizAnswerResponse?>>> GetUserQuizAnswerById(Guid userQuizAnswerId)
        {
            var result = await _userQuizAnswerService.GetUserQuizAnswerByIdAsync(userQuizAnswerId);
            return HandleResult(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Result<IEnumerable<UserQuizAnswerResponse>>>> GetUserQuizAnswersByUserId(Guid userId)
        {
            var result = await _userQuizAnswerService.GetUserQuizAnswersByUserIdAsync(userId);
            return HandleResult(result);
        }
        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<Result<IEnumerable<UserQuizAnswerResponse>>>> GetUserQuizAnswersByQuestionId(Guid questionId)
        {
            var result = await _userQuizAnswerService.GetUserQuizAnswersByQuestionIdAsync(questionId);
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<Result<UserQuizAnswerResponse>>> AddUserQuizAnswer([FromBody] UserQuizAnswerCreateRequest request)
        {
            var result = await _userQuizAnswerService.AddUserQuizAnswerAsync(request);
            return HandleResult(result);
        }
        [HttpPut("{userQuizAnswerId}")]
        public async Task<ActionResult<Result<UserQuizAnswerResponse>>> UpdateUserQuizAnswer(Guid userQuizAnswerId, [FromBody] UserQuizAnswerUpdateRequest request)
        {
            var result = await _userQuizAnswerService.UpdateUserQuizAnswerAsync(userQuizAnswerId, request);
            return HandleResult(result);
        }
        [HttpDelete("{userQuizAnswerId}")]
        public async Task<ActionResult<Result<UserQuizAnswerResponse>>> DeleteUserQuizAnswer(Guid userQuizAnswerId)
        {
            var result = await _userQuizAnswerService.DeleteUserQuizAnswerAsync(userQuizAnswerId);
            return HandleResult(result);
        }
    }
}
