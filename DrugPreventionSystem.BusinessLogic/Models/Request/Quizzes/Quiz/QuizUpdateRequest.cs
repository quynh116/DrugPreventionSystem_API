using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.Quiz
{
    public class QuizUpdateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? TotalQuestions { get; set; }
        public float? PassingScore { get; set; }
    }

}
