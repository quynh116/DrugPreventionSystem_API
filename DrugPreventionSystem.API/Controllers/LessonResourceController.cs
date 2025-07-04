using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
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
    public class LessonResourceController : BaseApiController
    {
        private readonly ILessonResourceService _lessonResourceService;

        public LessonResourceController(ILessonResourceService lessonResourceService)
        {
            _lessonResourceService = lessonResourceService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<LessonResourceResponse>>>> GetAll()
        {
            var result = await _lessonResourceService.GetAllLessonResourcesAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<LessonResourceResponse>>> GetById(Guid id)
        {
            var result = await _lessonResourceService.GetLessonResourceByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("byLesson/{lessonId}")]
        public async Task<ActionResult<Result<IEnumerable<LessonResourceResponse>>>> GetResourcesByLessonId(Guid lessonId)
        {
            var result = await _lessonResourceService.GetResourcesByLessonIdAsync(lessonId);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<LessonResourceResponse>>> Create([FromBody] LessonResourceRequest lessonResource)
        {
            var result = await _lessonResourceService.AddNewLessonResourceAsync(lessonResource);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<LessonResourceResponse>>> Update(Guid id, [FromBody] LessonResourceRequest lessonResource)
        {
            var result = await _lessonResourceService.UpdateLessonResourceAsync(id, lessonResource);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> Delete(Guid id)
        {
            var result = await _lessonResourceService.DeleteLessonResourceByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return NoContent();
            return HandleResult(result);
        }
    }
} 