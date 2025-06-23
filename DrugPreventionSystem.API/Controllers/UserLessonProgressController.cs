using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserLessonProgress;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserLessonProgress;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLessonProgressController : BaseApiController
    {
        private readonly IUserLessonProgressService _userLessonProgressService;

        public UserLessonProgressController(IUserLessonProgressService userLessonProgressService)
        {
            _userLessonProgressService = userLessonProgressService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserLessonProgressResponse>>>> GetUserLessonProgressesAsync()
        {
            var result = await _userLessonProgressService.GetUserLessonProgressesAsync();
            return HandleResult(result);
        }
        [HttpGet("{progressId}")]
        public async Task<ActionResult<Result<UserLessonProgressResponse?>>> GetUserLessonProgressByIdAsync(Guid progressId)
        {
            var result = await _userLessonProgressService.GetUserLessonProgressByIdAsync(progressId);
            return HandleResult(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Result<IEnumerable<UserLessonProgressResponse>>>> GetUserLessonProgressByUserIdAsync(Guid userId)
        {
            var result = await _userLessonProgressService.GetUserLessonProgressByUserIdAsync(userId);
            return HandleResult(result);
        }
        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<Result<IEnumerable<UserLessonProgressResponse>>>> GetUserLessonProgressByLessonIdAsync(Guid lessonId)
        {
            var result = await _userLessonProgressService.GetUserLessonProgressByLessonIdAsync(lessonId);
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<Result<UserLessonProgressResponse>>> AddUserLessonProgressAsync([FromBody] UserLessonProgressCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserLessonProgressResponse>.Invalid("Invalid creation data.", errors));
            }
            var result = await _userLessonProgressService.AddUserLessonProgressAsync(request);
            return HandleResult(result);
        }
        [HttpPut("{progressId}")]
        public async Task<ActionResult<Result<UserLessonProgressResponse>>> UpdateUserLessonProgressAsync(Guid progressId, [FromBody] UserLessonProgressUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserLessonProgressResponse>.Invalid("Invalid update data.", errors));
            }
            var result = await _userLessonProgressService.UpdateUserLessonProgressAsync(progressId, request);
            return HandleResult(result);
        }
        [HttpDelete("{progressId}")]
        public async Task<ActionResult<Result<UserLessonProgressResponse>>> DeleteUserLessonProgressAsync(Guid progressId)
        {
            var result = await _userLessonProgressService.DeleteUserLessonProgressAsync(progressId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
