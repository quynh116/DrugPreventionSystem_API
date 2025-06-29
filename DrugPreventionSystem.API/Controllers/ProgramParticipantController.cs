using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramParticipantController : BaseApiController
    {
        private readonly IProgramParticipantService _participantService;

        public ProgramParticipantController(IProgramParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost]
        public async Task<ActionResult<Result<ProgramParticipantResponse>>> AddAsync([FromBody] ProgramParticipantCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(Result<ProgramParticipantResponse>.Invalid("Invalid participant data", errors));
            }

            var result = await _participantService.CreateAsync(request);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<ProgramParticipantResponse>>>> GetAllAsync()
        {
            var result = await _participantService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ProgramParticipantResponse>>> GetByIdAsync(Guid id)
        {
            var result = await _participantService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<ProgramParticipantResponse>>> UpdateAsync(Guid id, [FromBody] ProgramParticipantUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(Result<ProgramParticipantResponse>.Invalid("Invalid update data", errors));
            }

            var result = await _participantService.UpdateAsync(request, id);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteAsync(Guid id)
        {
            var result = await _participantService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
