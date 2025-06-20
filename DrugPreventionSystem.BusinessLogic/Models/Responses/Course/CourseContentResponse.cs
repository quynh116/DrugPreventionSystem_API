using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Course
{
    public class CourseContentResponse
    {
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public List<CourseWeekDto> CourseWeeks { get; set; } = new List<CourseWeekDto>();
    }

    public class CourseWeekDto
    {
        public Guid CourseWeekId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int WeekNumber { get; set; }
        public List<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }

    public class LessonDto
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? DurationMinutes { get; set; }
        public int Sequence { get; set; }
        
    }
}
