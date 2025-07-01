using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Lesson
{
    public class LessonRequest
    {
        public Guid WeekId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }

        public int? DurationMinutes { get; set; }

        public int Sequence { get; set; }

        public bool HasQuiz { get; set; } = false;

        public bool HasPractice { get; set; } = false;


    }
}
