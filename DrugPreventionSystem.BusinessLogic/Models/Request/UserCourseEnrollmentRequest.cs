using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class UserCourseEnrollmentRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        
    }
} 