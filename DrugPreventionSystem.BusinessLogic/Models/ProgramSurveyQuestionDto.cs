using System;

namespace DrugPreventionSystem.BusinessLogic.Models
{
    public class ProgramSurveyQuestionDto
    {
        public Guid QuestionId { get; set; }
        public Guid SurveyId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = "text";
    }
} 