using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
using DrugPreventionSystem.BusinessLogic.Models.Request.Instructor;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseApiController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpPost]
        public async Task<ActionResult<Result<CourseResponse>>> AddAsync([FromBody] CourseCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(Result<CourseResponse>.Invalid("Invalid course data", errors));
            }

            var result = await _courseService.CreateAsync(request);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<CourseResponse>>>> GetAllAsync()
        {
            var result= await _courseService.GetAllAsync();
            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<CourseResponse>>> GetByIdAsync(Guid id)
        {
            var result = await _courseService.GetByIdAsync(id);
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<CourseResponse>>> UpdateAsync(Guid id, [FromBody] CourseUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(Result<CourseResponse>.Invalid("Invalid update data",errors));
            }
            var result = await _courseService.UpdateAsync(request,id);
            return HandleResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteAsync(Guid id)
        {
            var result = await _courseService.DeleteAsync(id);
            if(result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }

    }
}

