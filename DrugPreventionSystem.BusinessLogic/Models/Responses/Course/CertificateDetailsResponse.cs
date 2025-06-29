using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Course
{
    public class CertificateDetailsResponse
    {
        public string CertificateId { get; set; } 
        public string CertificateUrl { get; set; } 
        public string UserName { get; set; } 
        public string CourseTitle { get; set; } 
        public DateTime CompletionDate { get; set; } 
        public string DurationWeeks { get; set; } 
        public string InstructorName { get; set; } 
        public string IssuingOrganization { get; set; } = "Trung tâm Phòng chống Ma túy Quốc gia";
    }
}
