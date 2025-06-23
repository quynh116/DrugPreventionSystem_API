using System;
using System.Collections.Generic;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Course
{
    public class CourseWeekResponse
    {
        public Guid WeekId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int WeekNumber { get; set; }
        public List<LessonResponse> Lessons { get; set; } = new List<LessonResponse>();
    }

} 