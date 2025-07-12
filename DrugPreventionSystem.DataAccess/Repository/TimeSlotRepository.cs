using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.DataAccess.Context;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrugPreventionSystem.DataAccess.Repository
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TimeSlot> AddTimeSlotAsync(TimeSlot timeSlot)
        {
            await _context.TimeSlots.AddAsync(timeSlot);
            await _context.SaveChangesAsync();
            return timeSlot;
        }

        public async Task DeleteTimeSlotAsync(Guid timeSlotId)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(timeSlotId);
            if(timeSlot != null)
            {
                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public async Task<TimeSlot?> GetTimeSlotByIdAsync(Guid timeSlotId)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(ts => ts.TimeSlotId == timeSlotId);
        }

        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsByConsultantIdAsync(Guid consultantId)
        {
            return await _context.TimeSlots.Where(ts => ts.ConsultantId == consultantId).ToListAsync();
        }

        public async Task<TimeSlot> UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);
            await _context.SaveChangesAsync();
            return timeSlot;
        }
    }
}
