using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController : BaseApiController
    {
        private readonly IConsultantService _consultantService;
        public ConsultantController(IConsultantService consultantService)
        {
            _consultantService = consultantService;
        }
        

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<ConsultantReadResponse>>>> GetAllConsultants()
        {
            var result = await _consultantService.GetAllConsultantAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ConsultantReadResponse>>> GetConsultantById(Guid id)
        {
            var result = await _consultantService.GetConsultantByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<ConsultantReadResponse>>> UpdateConsultant(Guid id, [FromBody] ConsultantUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<ConsultantUpdateResponse>.Invalid("Invalid update data.", errors));
            }

            var result = await _consultantService.UpdateConsultantAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteConsultant(Guid id)
        {
            var result = await _consultantService.DeleteConsultantAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
