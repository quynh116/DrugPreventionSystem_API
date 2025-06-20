using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
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
    public class CourseWeekController : BaseApiController
    {
        private readonly ICourseWeekService _courseWeekService;

        public CourseWeekController(ICourseWeekService courseWeekService)
        {
            _courseWeekService = courseWeekService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<CourseWeek>>>> GetAll()
        {
            var result = await _courseWeekService.GetAllCourseWeeksAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<CourseWeek>>> GetById(Guid id)
        {
            var result = await _courseWeekService.GetCourseWeekByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<CourseWeek>>> Create([FromBody] CourseWeekRequest courseWeek)
        {
            var result = await _courseWeekService.AddNewCourseWeekAsync(courseWeek);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<CourseWeek>>> Update(Guid id, [FromBody] CourseWeek courseWeek)
        {
            var result = await _courseWeekService.UpdateCourseWeekAsync(id, courseWeek);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> Delete(Guid id)
        {
            var result = await _courseWeekService.DeleteCourseWeekByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return NoContent();
            return HandleResult(result);
        }
    }
} 