using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Course
{
    public class UserCourseResponse
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public int ProgressPercentage { get; set; } 
        public string? InstructorName { get; set; }
        public int? DurationWeeks { get; set; }
        public DateTime? LastAccessed { get; set; }
        public bool HasCertificate { get; set; }
    }
}
