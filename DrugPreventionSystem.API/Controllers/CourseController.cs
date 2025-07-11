using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
using DrugPreventionSystem.BusinessLogic.Models.Request.Instructor;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Course;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
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

        [HttpGet("byAgeGroup")]
        public async Task<ActionResult<Result<IEnumerable<CourseResponse>>>> GetCoursesByAgeGroup([FromQuery] string ageGroup)
        {
            var result = await _courseService.GetCoursesByAgeGroupAsync(ageGroup);
            return HandleResult(result);
        }

        [HttpGet("{courseId}/weeks-with-lessons")]
        public async Task<ActionResult<Result<CourseContentResponse>>> GetWeeksWithLessonsByCourseId(Guid courseId)
        {
            var result = await _courseService.GetCourseContentAsync(courseId);
            return HandleResult(result);
        }

        [HttpGet("{courseId}/lesson-progress-details")]
        public async Task<ActionResult<Result<CourseProgressDetailResponse>>> GetCourseProgressDetails( Guid userId, Guid courseId)
        {
            

            var result = await _courseService.GetCourseProgressDetailsForUserAsync(courseId, userId);
            return HandleResult(result);
        }

        [HttpGet("my-courses")]
        public async Task<ActionResult<Result<List<UserCourseResponse>>>> GetMyCourses(Guid userId)
        {


            var result = await _courseService.GetMyCoursesAsync(userId);
            return HandleResult(result);
        }

        [HttpGet("{courseId}/detail-for-user")]
        public async Task<ActionResult<Result<CourseDetailForUserResponse>>> GetCourseDetailForUser(Guid userId, Guid courseId)
        {
            

            var result = await _courseService.GetCourseDetailForUserAsync(courseId, userId);
            return HandleResult(result);
        }

        [HttpGet("{id}/requirements")]
        public async Task<IActionResult> GetCourseRequirements(Guid id)
        {
            var result = await _courseService.GetByIdAsync(id);

            if (string.IsNullOrEmpty(result.Data?.Requirements))
            {
                return Ok(Result<string>.Success("Không có yêu cầu cụ thể nào được liệt kê."));
            }
            var requirementsList = result.Data.Requirements
        .Split(';', StringSplitOptions.RemoveEmptyEntries)
        .Select(r => r.Trim())
        .ToList();

            return Ok(Result<List<string>>.Success(requirementsList));



        }

        [HttpGet("{courseId}/manager-content")] 
        public async Task<ActionResult<Result<CourseContentForEditResponse>>> GetCourseContentForAdmin(Guid courseId)
        {
            var result = await _courseService.GetCourseContentForEditAsync(courseId);
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

