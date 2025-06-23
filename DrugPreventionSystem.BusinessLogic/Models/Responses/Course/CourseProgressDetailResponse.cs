using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Course
{
    public class CourseProgressDetailResponse
    {
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public List<CourseWeekProgressDto> CourseWeeks { get; set; } = new List<CourseWeekProgressDto>();
    }

    public class CourseWeekProgressDto
    {
        public Guid CourseWeekId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int WeekNumber { get; set; }
        public List<LessonProgressDto> Lessons { get; set; } = new List<LessonProgressDto>();
    }

    public class LessonProgressDto
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? DurationMinutes { get; set; }
        public int Sequence { get; set; }
        public bool IsCompleted { get; set; } = false; 
    }
}