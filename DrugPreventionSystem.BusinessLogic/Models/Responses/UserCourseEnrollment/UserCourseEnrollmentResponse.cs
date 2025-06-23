using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserCourseEnrollment
{
    public class UserCourseEnrollmentResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? CompletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}