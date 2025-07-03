using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Appointment;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Appointment;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        private AppointmentResponse MapToAppoinmentResponse(Appointment appointment)
        {
            if (appointment == null)
            {
                return null;
            }
            return new AppointmentResponse
            {
                AppointmentId = appointment.AppointmentId,
                UserId = appointment.UserId,
                ConsultantId = appointment.ConsultantId,
                TimeSlotId = appointment.TimeSlotId,
                ReasonForConsultation = appointment.ReasonForConsultation,
                ConsultationType = appointment.ConsultationType,
                HasPreviousConsultation = appointment.HasPreviousConsultation,
                Status = appointment.Status,
                Notes = appointment.Notes,
                MeetUrl = appointment.MeetUrl,
                CreatedAt = appointment.CreatedAt,
                UpdatedAt = appointment.UpdatedAt
            };
        }
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<AppointmentResponse>> AddAppointmentAsync(AppointmentCreateRequest request)
        {
            try
            {
                if (request.UserId == Guid.Empty)
                {
                    return Result<AppointmentResponse>.Invalid("User ID is required.");
                }
                if (request.ConsultantId == Guid.Empty)
                {
                    return Result<AppointmentResponse>.Invalid("Consultant ID is required.");
                }
                if (request.TimeSlotId == Guid.Empty)
                {
                    return Result<AppointmentResponse>.Invalid("Time slot ID is required.");
                }
                if (string.IsNullOrEmpty(request.ReasonForConsultation))
                {
                    return Result<AppointmentResponse>.Invalid("Reason for consultation is required.");
                }
                var appointment = new Appointment
                {
                    UserId = request.UserId,
                    ConsultantId = request.ConsultantId,
                    TimeSlotId = request.TimeSlotId,
                    ReasonForConsultation = request.ReasonForConsultation,
                    ConsultationType = request.ConsultationType,
                    HasPreviousConsultation = request.HasPreviousConsultation,
                    Status = request.Status,
                    Notes = request.Notes,
                    MeetUrl = request.MeetUrl
                };
                var createdAppointment = await _appointmentRepository.AddAppointmentAsync(appointment);
                return Result<AppointmentResponse>.Success(MapToAppoinmentResponse(createdAppointment));
            }
            catch (Exception ex)
            {
                return Result<AppointmentResponse>.Error(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteAppointmentAsync(Guid appointmentId)
        {
            try
            {
                if (appointmentId == Guid.Empty)
                {
                    return Result<bool>.Invalid("Appointment ID is required.");
                }
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
                if (appointment == null)
                {
                    return Result<bool>.NotFound("Appointment not found.");
                }
                await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
                return Result<bool>.Success(true, "Appointment deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting appointment: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<AppointmentResponse>>> GetAllAppointmentsAsync()
        {
            try
            {
                var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
                if (appointments == null || !appointments.Any())
                {
                    return Result<IEnumerable<AppointmentResponse>>.NotFound("No appointments found.");
                }
                var appointmentResponses = appointments.Select(t => MapToAppoinmentResponse(t));
                return Result<IEnumerable<AppointmentResponse>>.Success(appointmentResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AppointmentResponse>>.Error($"Error retrieving appointments: {ex.Message}");
            }
        }
        public async Task<Result<AppointmentResponse?>> GetAppointmentByIdAsync(Guid appointmentId)
        {
            try
            {
                if (appointmentId == Guid.Empty)
                {
                    return Result<AppointmentResponse?>.Invalid("AppointmentId is required");
                }
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
                if (appointment == null)
                {
                    return Result<AppointmentResponse?>.NotFound("Appointment not found.");
                }
                return Result<AppointmentResponse?>.Success(MapToAppoinmentResponse(appointment));
            }
            catch (Exception ex)
            {
                return Result<AppointmentResponse?>.Error($"Error retrieving appointment: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<AppointmentResponse>>> GetAppointmentsByUserIdAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return Result<IEnumerable<AppointmentResponse>>.Invalid("User ID is required.");
                }
                var appointments = await _appointmentRepository.GetAppointmentsByUserIdAsync(userId);
                if (appointments == null || !appointments.Any())
                {
                    return Result<IEnumerable<AppointmentResponse>>.NotFound("No appointments found for this user.");
                }
                var appointmentResponses = appointments.Select(t => MapToAppoinmentResponse(t));
                return Result<IEnumerable<AppointmentResponse>>.Success(appointmentResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AppointmentResponse>>.Error($"Error retrieving appointments: {ex.Message}");
            }
        }

        public async Task<Result<AppointmentResponse>> UpdateAppointmentAsync(AppointmentUpdateRequest request, Guid appoinmentId)
        {
            try
            {
                if (appoinmentId == Guid.Empty)
                {
                    return Result<AppointmentResponse>.Invalid("Appointment ID is required.");
                }
                var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appoinmentId);
                if (existingAppointment == null)
                {
                    return Result<AppointmentResponse>.NotFound("Appointment not found.");
                }

                existingAppointment.UserId = request.UserId;
                existingAppointment.ConsultantId = request.ConsultantId;
                existingAppointment.TimeSlotId = request.TimeSlotId;
                existingAppointment.ReasonForConsultation = request.ReasonForConsultation;
                existingAppointment.ConsultationType = request.ConsultationType;
                existingAppointment.HasPreviousConsultation = request.HasPreviousConsultation;
                existingAppointment.Status = request.Status;
                existingAppointment.Notes = request.Notes;
                existingAppointment.MeetUrl = request.MeetUrl;
                var updatedAppointment = await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
                return Result<AppointmentResponse>.Success(MapToAppoinmentResponse(updatedAppointment));
            }
            catch (Exception ex)
            {
                return Result<AppointmentResponse>.Error($"Error updating appointment: {ex.Message}");
            }
        }
    }

}

