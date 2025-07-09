using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Appointment;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Appointment;
using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : BaseApiController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<AppointmentResponse>>>> GetAllAppointments()
        {
            var result = await _appointmentService.GetAllAppointmentsAsync();
            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<AppointmentResponse>>> GetAppointmentById(Guid id)
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id);
            return HandleResult(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Result<IEnumerable<AppointmentResponse>>>> GetAppointmentsByUserId(Guid userId)
        {
            var result = await _appointmentService.GetAppointmentsByUserIdAsync(userId);
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<Result<AppointmentResponse>>> AddAppointment([FromBody] AppointmentCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<AppointmentResponse>.Invalid("Invalid appointment data.", errors));
            }
            var result = await _appointmentService.AddAppointmentAsync(request);
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<AppointmentResponse>>> UpdateAppointment(Guid id, [FromBody] AppointmentUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<AppointmentResponse>.Invalid("Invalid appointment data.", errors));
            }
            var result = await _appointmentService.UpdateAppointmentAsync(request, id);
            return HandleResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteAppointment(Guid id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return HandleResult(result);
        }
    }
}
