using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrugPreventionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramFeedbackController : ControllerBase
    {
        private readonly IProgramFeedbackService _service;
        public ProgramFeedbackController(IProgramFeedbackService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramFeedbackCreateRequest request)
        {
            var result = await _service.CreateAsync(request);
            if (result == null) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProgramFeedbackUpdateRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (result == null) return NotFound(result);
            return Ok(result);
        }
    }
} 