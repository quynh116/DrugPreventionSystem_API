using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyResponse;
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

        public SurveyQuestionController(ISurveyQuestionService surveyQuestionService, ISurveyQuestionRepository surveyQuestionRepository)
        {
            _surveyQuestionService = surveyQuestionService;
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

        [HttpGet("{surveyId}/questions")]
        public async Task<ActionResult<Result<IEnumerable<SurveyQuestionResponse>>>> GetSurveyQuestionsBySurveyId(Guid surveyId)
        {
            var result = await _surveyQuestionService.GetSurveyQuestionsBySurveyIdAsync(surveyId);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<SurveyQuestionResponse>>> Add([FromBody] SurveyQuestionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<SurveyQuestionResponse>.Invalid("Invalid response data.", errors));
            }
            var result = await _surveyQuestionService.AddSurveyQuestionAsync(request);
            return HandleResult(result);
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
