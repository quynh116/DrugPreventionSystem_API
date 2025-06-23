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
        public string? VideoUrl { get; set; }
        public string? Description { get; set; } // Mô tả của bài học hoặc video

        public Guid CourseId { get; set; } 
        public string CourseTitle { get; set; } = string.Empty; 
        public float CourseProgressPercentage { get; set; } 

        public bool IsCompleted { get; set; }
    }
}
