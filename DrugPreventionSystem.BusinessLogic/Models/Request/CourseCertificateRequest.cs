using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class CourseCertificateRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime? IssuedAt { get; set; }
        public string? CertificateUrl { get; set; }
    }
} 