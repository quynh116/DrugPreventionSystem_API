using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizOption
{
    public class QuizOptionUpdateRequest
    {
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

}
