using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Consultant
{
    public class ConsultantUpdateRequest
    {
        public string LicenseNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int? YearsOfExperience { get; set; }
        public string Qualifications { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public decimal? ConsultationFee { get; set; }
        public bool? IsAvailable { get; set; }
        public string WorkingHours { get; set; } = string.Empty;
    }
}
