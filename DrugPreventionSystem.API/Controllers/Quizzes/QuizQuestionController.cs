using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers.Quizzes
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizQuestionController : BaseApiController
    {
        private readonly IQuizQuestionService _quizQuestionService;

        public QuizQuestionController(IQuizQuestionService quizQuestionService)
        {
            _quizQuestionService = quizQuestionService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<QuizQuestionResponse>>>> GetAllQuestions()
        {
            var result = await _quizQuestionService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<QuizQuestionResponse>>> GetQuestionById(Guid id)
        {
            var result = await _quizQuestionService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<QuizQuestionResponse>>> CreateQuestion([FromBody] QuizQuestionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<QuizQuestionResponse>.Invalid("Invalid question creation data.", errors));
            }

            var result = await _quizQuestionService.CreateAsync(request);
            return CreatedAtAction(nameof(GetQuestionById), new { id = result.Data?.QuestionId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<QuizQuestionResponse>>> UpdateQuestion(Guid id, [FromBody] QuizQuestionUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<QuizQuestionResponse>.Invalid("Invalid question update data.", errors));
            }

            var result = await _quizQuestionService.UpdateAsync(request, id);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteQuestion(Guid id)
        {
            var result = await _quizQuestionService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }

}
