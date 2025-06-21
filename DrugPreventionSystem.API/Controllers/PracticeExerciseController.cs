using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeExerciseController : BaseApiController
    {
        private readonly IPracticeExerciseService _service;
        public PracticeExerciseController(IPracticeExerciseService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<Result<PracticeExerciseResponse>>> Create([FromBody] PracticeExerciseRequest request)
        {
            var result = await _service.AddAsync(request);
            return HandleResult(result);
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<PracticeExerciseResponse>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<PracticeExerciseResponse>>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<PracticeExerciseResponse>>> Update(Guid id, [FromBody] PracticeExerciseRequest request)
        {
            var result = await _service.UpdateAsync(id, request);
            return HandleResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return NoContent();
            return HandleResult(result);
        }
        [HttpGet("byLesson/{lessonId}")]
        public async Task<ActionResult<Result<IEnumerable<PracticeExerciseResponse>>>> GetByLessonId(Guid lessonId)
        {
            var result = await _service.GetByLessonIdAsync(lessonId);
            return HandleResult(result);
        }
    }
} 