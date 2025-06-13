using DrugPreventionSystem.BusinessLogic.Models;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SurveyOptionController : ControllerBase
    {
        private readonly ISurveyOptionService _surveyOptionService;

        public SurveyOptionController(ISurveyOptionService surveyOptionService)
        {
            _surveyOptionService = surveyOptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyOptionDTO>>> GetAll()
        {
            var surveyOptions = await _surveyOptionService.GetAllAsync();
            return Ok(surveyOptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyOptionDTO>> GetById(Guid id)
        {
            var surveyOption = await _surveyOptionService.GetByIdAsync(id);
            if (surveyOption == null)
                return NotFound();

            return Ok(surveyOption);
        }


        [HttpPost]
        public async Task<ActionResult<SurveyOptionDTO>> Create(SurveyOptionDTO surveyOptionDTO)
        {
            var createdOption = await _surveyOptionService.CreateAsync(surveyOptionDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdOption.OptionId }, createdOption);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SurveyOptionDTO>> Update(Guid id, SurveyOptionDTO surveyOptionDTO)
        {
            try
            {
                var updatedOption = await _surveyOptionService.UpdateAsync(id, surveyOptionDTO);
                return Ok(updatedOption);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _surveyOptionService.DeleteAsync(id);
            return NoContent();
        }
    }
} 