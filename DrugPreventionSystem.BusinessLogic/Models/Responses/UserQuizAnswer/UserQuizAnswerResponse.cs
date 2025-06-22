using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserQuizAnswer
{
    public class UserQuizAnswerResponse
    {
        public Guid UserQuizAnswerId { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? SelectedOptionId { get; set; }
        public string? AnswerText { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.Now;
    }
}
