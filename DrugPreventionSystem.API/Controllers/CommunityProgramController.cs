using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityProgramController : BaseApiController
    {
        private readonly ICommunityProgramService _communityProgramService;

        public CommunityProgramController(ICommunityProgramService communityProgramService)
        {
            _communityProgramService = communityProgramService;
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<CommunityProgramResponse>>>> GetAllCommunityPrograms()
        {
            var result = await _communityProgramService.GetAllPrograms();
            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<CommunityProgramResponse>>> GetCommunityProgramById(Guid id)
        {
            var result = await _communityProgramService.GetProgramById(id);
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<Result<CommunityProgramResponse>>> AddCommunityProgram([FromBody] CommunityProgramCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<CommunityProgramResponse>.Invalid("Invalid program data.", errors));
            }
            var result = await _communityProgramService.AddCommunityProgram(request);
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<CommunityProgramResponse>>> UpdateCommunityProgram(Guid id, [FromBody] CommunityProgramUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<CommunityProgramResponse>.Invalid("Invalid program data.", errors));
            }
            var result = await _communityProgramService.UpdateCommunityProgram(request, id);
            return HandleResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteCommunityProgram(Guid id)
        {
            var result = await _communityProgramService.DeleteCommunityProgram(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
