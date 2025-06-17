using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Survey;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : BaseApiController
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<SurveyResponse>>>> GetAllSurvey()
        {
            var result = await _surveyService.GetAllSurveyAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<SurveyResponse>>> GetSurveyById(Guid id)
        {
            var result = await _surveyService.GetSurveyByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<SurveyResponse>>> UpdateSurveyById(Guid id, [FromBody] SurveyUpdateRequest request)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<SurveyResponse>.Invalid("Invalid update data.", errors));
            }
            var result = await _surveyService.UpdateSurveyAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteSurveyById(Guid id)
        {
            var result = await _surveyService.DeleteSurveyByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<SurveyResponse>>> AddNewSurvey([FromBody] SurveyCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<SurveyResponse>.Invalid("Invalid survey data.", errors));
            }
            var result = await _surveyService.AddNewSurveyAsync(request);
            return HandleResult(result);
        }
    }
}