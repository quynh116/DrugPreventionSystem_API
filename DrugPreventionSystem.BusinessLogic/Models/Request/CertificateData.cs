using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class CertificateData
    {
        public string UserName { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public DateTime CompletionDate { get; set; }
        public string DurationWeeks { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
    }
}
