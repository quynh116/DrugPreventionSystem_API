using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Appointment
{
    public class AppointmentCreateRequest
    {
        public Guid UserId { get; set; }
        public Guid ConsultantId { get; set; }
        public Guid TimeSlotId { get; set; }
        public string ReasonForConsultation { get; set; } = string.Empty;
        public string ConsultationType { get; set; } = string.Empty;
        public bool HasPreviousConsultation { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Notes { get; set; }
        public string MeetUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
