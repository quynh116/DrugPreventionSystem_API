using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Instructor;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class InstructorController : BaseApiController
        {
            private readonly IInstructorService _instructorService;
            public InstructorController(IInstructorService instructorService)
            {
                _instructorService = instructorService;
            }
        [HttpPost]
        public async Task<ActionResult<Result<InstructorResponse>>> AddAsync([FromBody] InstructorCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(Result<InstructorResponse>.Invalid("Invalid instrustor data", errors));
            }

            var result = await _instructorService.CreateAsync(request);
            return HandleResult(result);
        }


        [HttpGet]
            public async Task<ActionResult<Result<IEnumerable<InstructorResponse>>>> GetAllAsync()
            {
                var result = await _instructorService.GetAllAsync();
                return HandleResult(result);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Result<InstructorResponse>>> GetByIdAsync(Guid id)
            {
                var result = await _instructorService.GetByIdAsync(id);
                return HandleResult(result);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<Result<InstructorResponse>>> UpdateAsync(Guid id, [FromBody] InstructorUpdateRequest request)
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                    return BadRequest(Result<InstructorResponse>.Invalid("Invalid update data", errors));
                }

                var result = await _instructorService.UpdateAsync(request, id);
                return HandleResult(result);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<Result<bool>>> DeleteAsync(Guid id)
            {
                var result = await _instructorService.DeleteAsync(id);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return NoContent();
                }
                return HandleResult(result);
            }
        }

}

