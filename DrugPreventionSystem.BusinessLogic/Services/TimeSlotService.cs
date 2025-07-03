using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.TimeSlot;
using DrugPreventionSystem.BusinessLogic.Models.Responses.TimeSlot;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IConsultantRepository _consultantRepository;

        public TimeSlotService(ITimeSlotRepository timeSlotRepository, 
                                IConsultantRepository consultantRepository)
        {
            _timeSlotRepository = timeSlotRepository;
            _consultantRepository = consultantRepository;
        }
        private TimeSlotResponse MapToTimeSlotResponse(TimeSlot timeSlot)
        {
            if (timeSlot == null) return null;
            return new TimeSlotResponse
            {
                TimeSlotId = timeSlot.TimeSlotId,
                ConsultantId = timeSlot.ConsultantId,
                SlotDate = timeSlot.SlotDate,
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime,
                CreatedAt = timeSlot.CreatedAt,
                UpdatedAt = timeSlot.UpdatedAt
            };
        }
        public async Task<Result<TimeSlotResponse>> AddTimeSlotAsync(TimeSlotCreateRequest request)
        {
            try
            {
                if(request.ConsultantId == Guid.Empty)
                {
                    return Result<TimeSlotResponse>.NotFound("Consultant ID cannot be empty.");
                }
                var consultant = await _consultantRepository.GetConsultantByIdAsync(request.ConsultantId);
                if (consultant == null)
                {
                    return Result<TimeSlotResponse>.NotFound("Consultant not found.");
                }
                if(request.StartTime > request.EndTime)
                {
                    return Result<TimeSlotResponse>.Invalid("Start Time must be before End Time");
                }
                var timeSlot = new TimeSlot
                {
                    ConsultantId = request.ConsultantId,
                    SlotDate = request.SlotDate,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var createdTimeSlot = await _timeSlotRepository.AddTimeSlotAsync(timeSlot);
                return Result<TimeSlotResponse>.Success(MapToTimeSlotResponse(createdTimeSlot));
            }
            catch (Exception ex)
            {
                return Result<TimeSlotResponse>.Error($"An error occurred while adding the time slot: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteTimeSlotAsync(Guid timeSlotId)
        {
            try
            {
                if (timeSlotId == Guid.Empty)
                {
                    return Result<bool>.Invalid("Time slot ID cannot be empty.");
                }
                var timeSlot = await _timeSlotRepository.GetTimeSlotByIdAsync(timeSlotId);
                if (timeSlot == null)
                {
                    return Result<bool>.Invalid("Time slot not found.");
                }
                await _timeSlotRepository.DeleteTimeSlotAsync(timeSlotId);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"An error occurred while deleting the time slot: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<TimeSlotResponse>>> GetAllTimeSlotsAsync()
        {
            try
            {
                var timeSlots = await _timeSlotRepository.GetAllTimeSlotsAsync();
                if(timeSlots == null)
                {
                    return Result<IEnumerable<TimeSlotResponse>>.NotFound("No data to retrive");
                }
                var timeSlotResponses = timeSlots.Select(ts => MapToTimeSlotResponse(ts)).ToList();
                return Result<IEnumerable<TimeSlotResponse>>.Success(timeSlotResponses, "Time slot retrived successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TimeSlotResponse>>.Error($"An error occurred while retrieving all time slots: {ex.Message}");
            }
        }

        public async Task<Result<TimeSlotResponse?>> GetTimeSlotByIdAsync(Guid timeSlotId)
        {
            try
            {
                if (timeSlotId == Guid.Empty)
                {
                    return Result<TimeSlotResponse?>.Invalid("Time slot ID cannot be empty.");
                }
                var timeSlot = await _timeSlotRepository.GetTimeSlotByIdAsync(timeSlotId);
                if (timeSlot == null)
                {
                    return Result<TimeSlotResponse?>.NotFound("Time slot not found.");
                }
                return Result<TimeSlotResponse?>.Success(MapToTimeSlotResponse(timeSlot));
            }
            catch (Exception ex)
            {
                return Result<TimeSlotResponse?>.Error($"An error occurred while retrieving the time slot: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<TimeSlotResponse>>> GetTimeSlotsByConsultantIdAsync(Guid consultantId)
        {
            try 
            {
                if (consultantId == Guid.Empty)
                {
                    return Result<IEnumerable<TimeSlotResponse>>.Invalid("Consultant ID cannot be empty.");
                }
                var timeSlots = await _timeSlotRepository.GetTimeSlotsByConsultantIdAsync(consultantId);
                if (timeSlots == null || !timeSlots.Any())
                {
                    return Result<IEnumerable<TimeSlotResponse>>.NotFound("No time slots found for the specified consultant.");
                }
                var timeSlotResponses = timeSlots.Select(ts => MapToTimeSlotResponse(ts)).ToList();
                return Result<IEnumerable<TimeSlotResponse>>.Success(timeSlotResponses, "Time slots retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TimeSlotResponse>>.Error($"An error occurred while retrieving time slots by consultant ID: {ex.Message}");
            }
        }

        public async Task<Result<TimeSlotResponse>> UpdateTimeSlotAsync(TimeSlotUpdateRequest request, Guid timeSlotId)
        {
            try
            {
                if (timeSlotId == Guid.Empty)
                {
                    return Result<TimeSlotResponse>.Invalid("Time slot ID cannot be empty.");
                }
                var timeSlot = await _timeSlotRepository.GetTimeSlotByIdAsync(timeSlotId);
                if (timeSlot == null)
                {
                    return Result<TimeSlotResponse>.NotFound("Time slot not found.");
                }
                if (request.StartTime > request.EndTime)
                {
                    return Result<TimeSlotResponse>.Invalid("Start Time must be before End Time");
                }
                timeSlot.SlotDate = request.SlotDate;
                timeSlot.StartTime = request.StartTime;
                timeSlot.EndTime = request.EndTime;
                timeSlot.UpdatedAt = DateTime.UtcNow;
                var updatedTimeSlot = await _timeSlotRepository.UpdateTimeSlotAsync(timeSlot);
                return Result<TimeSlotResponse>.Success(MapToTimeSlotResponse(updatedTimeSlot), "Updated successfully");
            }
            catch (Exception ex)
            {
                return Result<TimeSlotResponse>.Error($"An error occurred while updating the time slot: {ex.Message}");
            }
        }
    }
}
