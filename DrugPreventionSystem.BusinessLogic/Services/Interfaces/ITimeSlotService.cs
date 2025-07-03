using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.TimeSlot;
using DrugPreventionSystem.BusinessLogic.Models.Responses.TimeSlot;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ITimeSlotService
    {
        Task<Result<IEnumerable<TimeSlotResponse>>> GetAllTimeSlotsAsync();
        Task<Result<TimeSlotResponse?>> GetTimeSlotByIdAsync(Guid timeSlotId);
        Task<Result<IEnumerable<TimeSlotResponse>>> GetTimeSlotsByConsultantIdAsync(Guid consultantId);
        Task<Result<TimeSlotResponse>> AddTimeSlotAsync(TimeSlotCreateRequest request);
        Task<Result<TimeSlotResponse>> UpdateTimeSlotAsync(TimeSlotUpdateRequest request, Guid timeSlotId);
        Task<Result<bool>> DeleteTimeSlotAsync(Guid timeSlotId);
    }
}
