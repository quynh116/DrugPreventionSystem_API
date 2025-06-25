using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz
{
    public class QuizResponse
    {
        public Guid QuizId { get; set; }
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? TotalQuestions { get; set; }
        public float? PassingScore { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
