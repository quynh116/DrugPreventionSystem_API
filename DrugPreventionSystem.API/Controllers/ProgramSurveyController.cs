using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using System.Linq;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;

namespace DrugPreventionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramSurveyController : ControllerBase
    {
        private readonly IProgramSurveyService _service;
        public ProgramSurveyController(IProgramSurveyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProgramSurveyDto>>> GetAll()
        {
            var surveys = await _service.GetAllDtoAsync();
            return Ok(surveys);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramSurveyDto>> GetById(Guid id)
        {
            var survey = await _service.GetDtoByIdAsync(id);
            if (survey == null) return NotFound();
            return Ok(survey);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProgramSurveyCreateRequest request)
        {
            var entity = new ProgramSurvey
            {
                SurveyId = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description
            };
            await _service.AddAsync(entity);
            var dto = new ProgramSurveyDto
            {
                SurveyId = entity.SurveyId,
                Title = entity.Title,
                Description = entity.Description
            };
            return CreatedAtAction(nameof(GetById), new { id = entity.SurveyId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProgramSurveyCreateRequest request)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            entity.Title = request.Title;
            entity.Description = request.Description;
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
