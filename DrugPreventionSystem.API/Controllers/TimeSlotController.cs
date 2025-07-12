using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.TimeSlot;
using DrugPreventionSystem.BusinessLogic.Models.Responses.TimeSlot;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : BaseApiController
    {
        private readonly ITimeSlotService _timeSlotService;

        public TimeSlotController(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<TimeSlotResponse>>>> GetAllTimeSlots()
        {
            var result = await _timeSlotService.GetAllTimeSlotsAsync();
            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<TimeSlotResponse>>> GetTimeSlotById(Guid id)
        {
            var result = await _timeSlotService.GetTimeSlotByIdAsync(id);
            return HandleResult(result);
        }
        [HttpGet("consultant/{consultantId}")]
        public async Task<ActionResult<Result<IEnumerable<TimeSlotResponse>>>> GetTimeSlotsByConsultantId(Guid consultantId)
        {
            var result = await _timeSlotService.GetTimeSlotsByConsultantIdAsync(consultantId);
            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<Result<TimeSlotResponse>>> AddTimeSlot([FromBody] TimeSlotCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<TimeSlotResponse>.Invalid("Invalid time slot data.", errors));
            }
            var result = await _timeSlotService.AddTimeSlotAsync(request);
            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<TimeSlotResponse>>> UpdateTimeSlot(Guid id, [FromBody] TimeSlotUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<TimeSlotResponse>.Invalid("Invalid time slot data.", errors));
            }
            var result = await _timeSlotService.UpdateTimeSlotAsync(request, id);
            return HandleResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteTimeSlot(Guid id)
        {
            var result = await _timeSlotService.DeleteTimeSlotAsync(id);
            return HandleResult(result);
        }
    }
}
