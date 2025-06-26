using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson
{
    public class LessonDetailResponse
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? DurationMinutes { get; set; }
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public string? Description { get; set; } 

        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public float CourseProgressPercentage { get; set; }

        public bool IsCompleted { get; set; }
        public ICollection<LessonResourceDto> Resources { get; set; } = new List<LessonResourceDto>();
    }

    public class LessonResourceDto // DTO cho từng tài nguyên
    {
        public Guid ResourceId { get; set; }
        public string ResourceType { get; set; } = string.Empty;
        public string ResourceUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
