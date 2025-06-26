using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes
{
    public class SubmitQuizRequest
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; } 
        public List<QuestionAnswerRequest> Answers { get; set; } = new List<QuestionAnswerRequest>();
    }

    public class QuestionAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public Guid? SelectedOptionId { get; set; } 
        public string? AnswerText { get; set; } 
    }
}
