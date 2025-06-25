using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Quizzes
{
    public class QuestionAndOptionResponse
    {
        public Guid QuizId { get; set; }
        public Guid LessonId { get; set; }
        public ICollection<QuizQuestionResponse1> Questions { get; set; } = new List<QuizQuestionResponse1>();
    }

    public class QuizQuestionResponse1
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? QuestionType { get; set; }
        public int Sequence { get; set; }
        public ICollection<QuizOptionResponse1> Options { get; set; } = new List<QuizOptionResponse1>();
    }
    public class QuizOptionResponse1
    {
        public Guid OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
