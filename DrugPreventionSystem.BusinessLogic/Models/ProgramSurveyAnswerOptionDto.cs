using System;

namespace DrugPreventionSystem.BusinessLogic.Models
{
    public class ProgramSurveyAnswerOptionDto
    {
        public Guid OptionId { get; set; }
        public Guid QuestionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
    }
} 