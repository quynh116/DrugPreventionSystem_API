using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Survey;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyResponse;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSurveyResponseController : BaseApiController
    {
        private readonly IUserSurveyResponseService _userSurveyResponseService;

        public UserSurveyResponseController (IUserSurveyResponseService userSurveyResponseService)
        {
            _userSurveyResponseService = userSurveyResponseService;
        }
       
        [HttpPost]
        public async Task<ActionResult<Result<UserSurveyResponseResponse>>> Add([FromBody] UserSurveyResponseCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserSurveyResponseResponse>.Invalid("Invalid response data.", errors));
            }
            var result = await _userSurveyResponseService.CreateAsync(request);
            return HandleResult(result);
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserSurveyResponseResponse>>>> GetAll()
        {
            var result = await _userSurveyResponseService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserSurveyResponseResponse>>> GetById(Guid id)
        {
            var result = await _userSurveyResponseService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<UserSurveyResponseResponse>>> UpdateSurveyQuestion(Guid id, [FromBody] UserSurveyResponseUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<UserSurveyResponseResponse>.Invalid("Invalid update data.", errors));
            }

            var result = await _userSurveyResponseService.UpdateAsync(request, id);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteSurveyQuestion(Guid id)
        {
            var result = await _userSurveyResponseService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }

    }
}
