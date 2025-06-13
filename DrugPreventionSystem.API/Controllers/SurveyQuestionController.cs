using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyQuestionController : BaseApiController
    {
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;

        public SurveyQuestionController(ISurveyQuestionService surveyQuestionService, ISurveyQuestionRepository surveyQuestionRepository)
        {
            _surveyQuestionService = surveyQuestionService;
            _surveyQuestionRepository = surveyQuestionRepository;
        }

        private SurveyQuestionResponse MapToResponse(SurveyQuestion question)
        {
            return new SurveyQuestionResponse
            {
                QuestionId = question.QuestionId,
                SurveyId = question.SurveyId,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                Sequence = question.Sequence,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt
            };
        }



        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<SurveyQuestionResponse>>>> GetAllSurveyQuestions()
        {
            var result = await _surveyQuestionService.GetAllSurveyQuestionsAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<SurveyQuestionResponse>>> GetSurveyQuestionById(Guid id)
        {
            var result = await _surveyQuestionService.GetSurveyQuestionByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<Result<SurveyQuestionResponse>> AddSurveyQuestionAsync(SurveyQuestionCreateRequest request)
        {
          try
          {
            var newQuestion = new SurveyQuestion
            {
                QuestionId = Guid.NewGuid(),
                SurveyId = request.SurveyId,
                QuestionText = request.QuestionText,
                QuestionType = request.QuestionType,
                Sequence = request.Sequence,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _surveyQuestionRepository.AddSurveyQuestion(newQuestion);

            return Result<SurveyQuestionResponse>.Success(MapToResponse(newQuestion), "Survey question added.");
          }
          catch (Exception ex)
          {
            return Result<SurveyQuestionResponse>.Error($"Error adding survey question: {ex.Message}");
        }
    }


    [HttpPut("{id}")]
        public async Task<ActionResult<Result<SurveyQuestionResponse>>> UpdateSurveyQuestion(Guid id, [FromBody] SurveyQuestionUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<SurveyQuestionResponse>.Invalid("Invalid update data.", errors));
            }

            var result = await _surveyQuestionService.UpdateSurveyQuestionAsync(request,id);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteSurveyQuestion(Guid id)
        {
            var result = await _surveyQuestionService.DeleteSurveyQuestionAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
