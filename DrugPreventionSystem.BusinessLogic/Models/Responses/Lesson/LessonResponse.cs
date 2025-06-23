using System;
using System.Collections.Generic;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson
{
    public class LessonResponse
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? DurationMinutes { get; set; }
        public int Sequence { get; set; }
        public bool HasQuiz { get; set; }
        public bool HasPractice { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LessonResourceResponse> Resources { get; set; } = new List<LessonResourceResponse>();
    }
} 