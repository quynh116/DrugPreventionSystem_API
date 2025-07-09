using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class ProgramSurveyQuestionCreateRequest
    {
        public Guid SurveyId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = "text";
    }
} 