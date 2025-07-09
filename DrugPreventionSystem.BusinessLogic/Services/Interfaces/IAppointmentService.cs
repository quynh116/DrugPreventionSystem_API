using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Appointment;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Appointment;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<Result<IEnumerable<AppointmentResponse>>> GetAllAppointmentsAsync();
        Task<Result<AppointmentResponse?>> GetAppointmentByIdAsync(Guid appointmentId);
        Task<Result<IEnumerable<AppointmentResponse>>> GetAppointmentsByUserIdAsync(Guid userId);
        Task<Result<AppointmentResponse>> AddAppointmentAsync(AppointmentCreateRequest request);
        Task<Result<AppointmentResponse>> UpdateAppointmentAsync(AppointmentUpdateRequest request, Guid appoinmentId);
        Task<Result<bool>> DeleteAppointmentAsync(Guid appointmentId);
    }
}
