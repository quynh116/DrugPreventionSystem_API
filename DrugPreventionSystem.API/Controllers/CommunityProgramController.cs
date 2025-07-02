using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Services;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityProgramController : BaseApiController
    {
        private readonly ICommunityProgramService _communityProgramService;
        private readonly IProgramService _programService;

        public CommunityProgramController(ICommunityProgramService communityProgramService, IProgramService programService)
        {
            _communityProgramService = communityProgramService;
            _programService = programService;
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
        [HttpPost("register")]
        public async Task<IActionResult> RegisterForProgram([FromBody] ProgramParticipantCreateRequest request)
        {
            try
            {
                var result = await _programService.RegisterForProgramAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet("user/{userId}/enrolled")]
        public async Task<IActionResult> GetProgramsUserEnrolled(Guid userId)
        {
            var result = await _programService.GetProgramsUserEnrolledAsync(userId);
            return Ok(result);
        }

        [HttpPost("feedback")]
        public async Task<IActionResult> SubmitProgramFeedback([FromBody] ProgramFeedbackCreateRequest request)
        {
            var result = await _programService.SubmitProgramFeedbackAsync(request);
            return Ok(result);
        }

        [HttpDelete("cancel")]
        public async Task<IActionResult> CancelRegistration([FromQuery] Guid programId, [FromQuery] Guid userId)
        {
            try
            {
                var request = new ProgramParticipantCancelRequest { ProgramId = programId, UserId = userId };
                await _programService.CancelRegistrationAsync(request);
                return Ok(new { message = "Cancel registration successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}
