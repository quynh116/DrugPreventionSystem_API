using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram
{
    public class ProgramSurveyResponseDto
    {
        public Guid ResponseId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<ProgramSurveyAnswerDto1> Answers { get; set; } = new();
    }

    public class ProgramSurveyAnswerDto1
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = "text";
        public string? AnswerText { get; set; }
        public Guid? SelectedOptionId { get; set; }
        public string? SelectedOptionText { get; set; }
    }
}
