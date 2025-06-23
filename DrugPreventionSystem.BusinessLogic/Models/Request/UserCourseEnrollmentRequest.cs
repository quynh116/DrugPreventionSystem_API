using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class UserCourseEnrollmentRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 