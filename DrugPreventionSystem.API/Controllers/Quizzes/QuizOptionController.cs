using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizOption;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers.Quizzes
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizOptionController : BaseApiController
    {
        private readonly IQuizOptionService _quizOptionService;

        public QuizOptionController(IQuizOptionService quizOptionService)
        {
            _quizOptionService = quizOptionService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<QuizOptionResponse>>>> GetAllOptions()
        {
            var result = await _quizOptionService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<QuizOptionResponse>>> GetOptionById(Guid id)
        {
            var result = await _quizOptionService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<QuizOptionResponse>>> CreateOption([FromBody] QuizOptionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<QuizOptionResponse>.Invalid("Invalid option creation data.", errors));
            }

            var result = await _quizOptionService.CreateAsync(request);
            return CreatedAtAction(nameof(GetOptionById), new { id = result.Data?.OptionId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<QuizOptionResponse>>> UpdateOption(Guid id, [FromBody] QuizOptionUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<QuizOptionResponse>.Invalid("Invalid option update data.", errors));
            }

            var result = await _quizOptionService.UpdateAsync(request, id);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteOption(Guid id)
        {
            var result = await _quizOptionService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }

}
