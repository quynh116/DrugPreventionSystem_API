using System;
using System.Collections.Generic;

namespace DrugPreventionSystem.BusinessLogic.Models
{
    public class ProgramSurveyQuestionDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = "text";
        public List<ProgramSurveyAnswerOptionDto> AnswerOptions { get; set; } = new();
    }
} 