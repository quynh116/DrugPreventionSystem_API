using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync();
        Task<TimeSlot?> GetTimeSlotByIdAsync(Guid timeSlotId);
        Task<IEnumerable<TimeSlot>> GetTimeSlotsByConsultantIdAsync(Guid consultantId);
        Task<TimeSlot> AddTimeSlotAsync(TimeSlot timeSlot);
        Task<TimeSlot> UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task DeleteTimeSlotAsync(Guid timeSlotId);
    }
}
