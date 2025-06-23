using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.Quiz;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers.Quizzes
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : BaseApiController
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<QuizResponse>>>> GetAllQuizzes()
        {
            var result = await _quizService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<QuizResponse>>> GetQuizById(Guid id)
        {
            var result = await _quizService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<QuizResponse>>> CreateQuiz([FromBody] QuizCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<QuizResponse>.Invalid("Invalid quiz creation data.", errors));
            }

            var result = await _quizService.CreateAsync(request);
            return CreatedAtAction(nameof(GetQuizById), new { id = result.Data?.QuizId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<QuizResponse>>> UpdateQuiz(Guid id, [FromBody] QuizUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<QuizResponse>.Invalid("Invalid quiz update data.", errors));
            }

            var result = await _quizService.UpdateAsync(request, id);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteQuiz(Guid id)
        {
            var result = await _quizService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }

}
