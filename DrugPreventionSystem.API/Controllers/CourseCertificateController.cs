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
    public class CourseCertificateController : BaseApiController
    {
        private readonly ICourseCertificateService _service;
        public CourseCertificateController(ICourseCertificateService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<Result<CourseCertificateResponse>>> Create([FromBody] CourseCertificateRequest request)
        {
            var result = await _service.AddAsync(request);
            return HandleResult(result);
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<CourseCertificateResponse>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<CourseCertificateResponse>>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<CourseCertificateResponse>>> Update(Guid id, [FromBody] CourseCertificateRequest request)
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
        public async Task<ActionResult<Result<IEnumerable<CourseCertificateResponse>>>> GetByUserId(Guid userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return HandleResult(result);
        }
        [HttpGet("byCourse/{courseId}")]
        public async Task<ActionResult<Result<IEnumerable<CourseCertificateResponse>>>> GetByCourseId(Guid courseId)
        {
            var result = await _service.GetByCourseIdAsync(courseId);
            return HandleResult(result);
        }
    }
} 