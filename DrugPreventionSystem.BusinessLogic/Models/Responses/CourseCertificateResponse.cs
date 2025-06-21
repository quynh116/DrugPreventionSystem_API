using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class CourseCertificateResponse
    {
        public Guid CertificateId { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime IssuedAt { get; set; }
        public string? CertificateUrl { get; set; }
    }
} 