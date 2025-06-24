using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizQuestion
{
    public class QuizQuestionUpdateRequest
    {
        public string QuestionText { get; set; } = string.Empty;
        public string? QuestionType { get; set; }
        public int Sequence { get; set; }
    }

}
