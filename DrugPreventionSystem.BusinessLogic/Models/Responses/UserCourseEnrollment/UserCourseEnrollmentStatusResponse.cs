using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserCourseEnrollment
{
    public class UserCourseEnrollmentStatusResponse
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public bool IsEnrolled { get; set; }
        public DateTime? EnrollmentDate { get; set; } 
    }
}
