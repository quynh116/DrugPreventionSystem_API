using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class UserCourseEnrollmentUpdateRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
