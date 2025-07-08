using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Request;

namespace DrugPreventionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramSurveyQuestionController : ControllerBase
    {
        private readonly IProgramSurveyQuestionService _service;
        public ProgramSurveyQuestionController(IProgramSurveyQuestionService service)
        {
            _service = service;
        }

        [HttpGet("survey/{surveyId}")]
        public async Task<ActionResult<List<ProgramSurveyQuestionDto>>> GetBySurveyId(Guid surveyId)
        {
            var questions = await _service.GetDtosBySurveyIdAsync(surveyId);
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramSurveyQuestionDto>> GetById(Guid id)
        {
            var question = await _service.GetDtoByIdAsync(id);
            if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProgramSurveyQuestionCreateRequest request)
        {
            var entity = new ProgramSurveyQuestion
            {
                QuestionId = Guid.NewGuid(),
                SurveyId = request.SurveyId,
                QuestionText = request.QuestionText,
                QuestionType = request.QuestionType
            };
            await _service.AddAsync(entity);
            var dto = new ProgramSurveyQuestionDto
            {
                QuestionId = entity.QuestionId,
                SurveyId = entity.SurveyId,
                QuestionText = entity.QuestionText,
                QuestionType = entity.QuestionType
            };
            return CreatedAtAction(nameof(GetById), new { id = entity.QuestionId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProgramSurveyQuestionCreateRequest request)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            entity.SurveyId = request.SurveyId;
            entity.QuestionText = request.QuestionText;
            entity.QuestionType = request.QuestionType;
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