using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyResponse
{
    public class SurveyQuestionAnswerResponse
    {
        public string QuestionText { get; set; } = string.Empty;
        public string ChosenAnswerText { get; set; } = string.Empty; 
        public int? Score { get; set; }
    }
}
