using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Course
{
    public class CourseUpdateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? AgeGroup { get; set; } // học sinh, sinh viên, phụ huynh, giáo viên, ...
        public bool IsActive { get; set; } = true;
        public Guid InstructorId { get; set; }
        public string? Requirements { get; set; }
    }
}
