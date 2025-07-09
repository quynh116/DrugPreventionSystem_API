using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class ProgramSurveyAnswerOptionCreateRequest
    {
        public Guid QuestionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
    }
} 