using DrugPreventionSystem.BusinessLogic.Models;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramSurveyAnswerOptionController : ControllerBase
    {
        private readonly IProgramSurveyAnswerOptionService _service;
        public ProgramSurveyAnswerOptionController(IProgramSurveyAnswerOptionService service)
        {
            _service = service;
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<List<ProgramSurveyAnswerOptionDto>>> GetByQuestionId(Guid questionId)
        {
            var options = await _service.GetDtosByQuestionIdAsync(questionId);
            return Ok(options);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramSurveyAnswerOptionDto>> GetById(Guid id)
        {
            var option = await _service.GetDtoByIdAsync(id);
            if (option == null) return NotFound();
            return Ok(option);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProgramSurveyAnswerOptionCreateRequest request)
        {
            var entity = new ProgramSurveyAnswerOption
            {
                OptionId = Guid.NewGuid(),
                QuestionId = request.QuestionId,
                OptionText = request.OptionText
            };
            await _service.AddAsync(entity);
            var dto = new ProgramSurveyAnswerOptionDto
            {
                OptionId = entity.OptionId,
                QuestionId = entity.QuestionId,
                OptionText = entity.OptionText
            };
            return CreatedAtAction(nameof(GetById), new { id = entity.OptionId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProgramSurveyAnswerOptionCreateRequest request)
        {
            // Lấy entity gốc từ service (hoặc repository)
            var dbEntity = await _service.GetEntityByIdAsync(id);
            if (dbEntity == null) return NotFound();
            dbEntity.QuestionId = request.QuestionId;
            dbEntity.OptionText = request.OptionText;
            await _service.UpdateAsync(dbEntity);
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