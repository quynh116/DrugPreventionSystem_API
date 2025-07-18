using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Appointment
{
    public class AppointmentResponse
    {
        public Guid AppointmentId { get; set; }
        public Guid UserId { get; set; }
        public Guid ConsultantId { get; set; }
        public Guid TimeSlotId { get; set; }
        public string ReasonForConsultation { get; set; }
        public string ConsultationType { get; set; }
        public bool HasPreviousConsultation { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Notes { get; set; }
        public string MeetUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
