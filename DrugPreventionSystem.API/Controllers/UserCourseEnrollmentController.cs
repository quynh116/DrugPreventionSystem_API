using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserCourseEnrollment;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCourseEnrollmentController : BaseApiController
    {
        private readonly IUserCourseEnrollmentService _service;
        public UserCourseEnrollmentController(IUserCourseEnrollmentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<Result<UserCourseEnrollmentResponse>>> Create([FromBody] UserCourseEnrollmentRequest request)
        {
            var result = await _service.AddAsync(request);
            return HandleResult(result);
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserCourseEnrollmentResponse>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserCourseEnrollmentResponse>>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<UserCourseEnrollmentResponse>>> Update(Guid id, [FromBody] UserCourseEnrollmentUpdateRequest request)
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
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<Result<IEnumerable<UserCourseEnrollmentResponse>>>> GetByUserId(Guid userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return HandleResult(result);
        }
        [HttpGet("byCourse/{courseId}")]
        public async Task<ActionResult<Result<IEnumerable<UserCourseEnrollmentResponse>>>> GetByCourseId(Guid courseId)
        {
            var result = await _service.GetByCourseIdAsync(courseId);
            return HandleResult(result);
        }

        [HttpGet("{courseId}/enrollment-status")]
        public async Task<ActionResult<Result<UserCourseEnrollmentStatusResponse>>> GetEnrollmentStatus(Guid userId, Guid courseId)
        {
            

            var result = await _service.CheckUserCourseEnrollmentStatusAsync(courseId, userId);
            return HandleResult(result);
        }

    }
}