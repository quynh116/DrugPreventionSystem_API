using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz
{
    public class QuizResultResponse
    {
        public Guid ResultId { get; set; } // ID của UserModuleQuizResult
        public Guid QuizId { get; set; }
        public Guid LessonId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;
        public float TotalScore { get; set; }
        public int CorrectCount { get; set; }
        public int TotalQuestions { get; set; }
        public string Status { get; set; } = string.Empty; // passed, failed
        public DateTime TakenAt { get; set; }
        public bool PassedQuizThreshold { get; set; } // Indicates if score met passing_score
        public ICollection<QuizResultQuestionResponse> QuestionsAttempted { get; set; } = new List<QuizResultQuestionResponse>();
    }
}
