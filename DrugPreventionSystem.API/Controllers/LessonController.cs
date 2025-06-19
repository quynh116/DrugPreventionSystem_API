using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : BaseApiController
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<Lesson>>>> GetAll()
        {
            var result = await _lessonService.GetAllLessonsAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<Lesson>>> GetById(Guid id)
        {
            var result = await _lessonService.GetLessonByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<Lesson>>> Create([FromBody] Lesson lesson)
        {
            var result = await _lessonService.AddNewLessonAsync(lesson);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<Lesson>>> Update(Guid id, [FromBody] Lesson lesson)
        {
            var result = await _lessonService.UpdateLessonAsync(id, lesson);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> Delete(Guid id)
        {
            var result = await _lessonService.DeleteLessonByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return NoContent();
            return HandleResult(result);
        }
    }
} 