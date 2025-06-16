using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyAnswer
{
    public class UserSurveyAnswerCreateRequest
    {
        public Guid ResponseId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid OptionId { get; set; } 
        public string? AnswerText { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.Now;
    }
}
