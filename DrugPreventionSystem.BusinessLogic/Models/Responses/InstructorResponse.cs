using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class InstructorResponse
    {
        public Guid InstructorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int? ExperienceYears { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
