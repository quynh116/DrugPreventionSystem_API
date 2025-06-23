using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserModuleQuizResult;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserModuleQuizResult;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserModuleQuizResultController : BaseApiController
    {
        private readonly IUserModuleQuizResultService _userModuleQuizResultService;

        public UserModuleQuizResultController(IUserModuleQuizResultService userModuleQuizResultService)
        {
            _userModuleQuizResultService = userModuleQuizResultService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserModuleQuizResultResponse>>>> GetUserModuleQuizResults()
        {
            var result = await _userModuleQuizResultService.GetUserModuleQuizResultsAsync();
            return HandleResult(result);
        }
        [HttpGet("{resultId}")]
        public async Task<ActionResult<Result<UserModuleQuizResultResponse?>>> GetUserModuleQuizResultById(Guid resultId)
        {
            var result = await _userModuleQuizResultService.GetUserModuleQuizResultByIdAsync(resultId);
            return HandleResult(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Result<IEnumerable<UserModuleQuizResultResponse>>>> GetUserModuleQuizResultsByUserId(Guid userId)
        {
            var result = await _userModuleQuizResultService.GetUserModuleQuizResultsByUserIdAsync(userId);
            return HandleResult(result);
        }
        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<Result<IEnumerable<UserModuleQuizResultResponse>>>> GetUserModuleQuizResultsByLessonId(Guid lessonId)
        {
            var result = await _userModuleQuizResultService.GetUserModuleQuizResultsByLessonIdAsync(lessonId);
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<Result<UserModuleQuizResultResponse>>> AddUserModuleQuizResult([FromBody] UserModuleQuizResultCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserModuleQuizResultResponse>.Invalid("Invalid creation data.", errors));
            }
            var result = await _userModuleQuizResultService.AddUserModuleQuizResultAsync(request);
            return HandleResult(result);
        }
        [HttpPut("{resultId}")]
        public async Task<ActionResult<Result<UserModuleQuizResultResponse>>> UpdateUserModuleQuizResult(Guid resultId, [FromBody] UserModuleQuizResultUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserModuleQuizResultResponse>.Invalid("Invalid update data.", errors));
            }
            var result = await _userModuleQuizResultService.UpdateUserModuleQuizResultAsync(resultId, request);
            return HandleResult(result);
        }
        [HttpDelete("{resultId}")]
        public async Task<ActionResult<Result<UserModuleQuizResultResponse>>> DeleteUserModuleQuizResult(Guid resultId)
        {
            var result = await _userModuleQuizResultService.DeleteUserModuleQuizResultAsync(resultId);
            return HandleResult(result);
        }
    }
}