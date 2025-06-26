using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz
{
    public class QuizResultQuestionResponse
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? QuestionType { get; set; } 
        public int Sequence { get; set; }

        
        public Guid? UserSelectedOptionId { get; set; }
        public string? UserSelectedOptionText { get; set; } 

        
        public bool IsUserAnswerCorrect { get; set; }
    }
}
