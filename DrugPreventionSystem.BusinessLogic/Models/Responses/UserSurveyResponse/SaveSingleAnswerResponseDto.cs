using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyResponse
{
    public class SaveSingleAnswerResponseDto
    {
        public Guid AnswerId { get; set; }
        public Guid ResponseId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int? ScoreValue { get; set; }
        public DateTime AnsweredAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
