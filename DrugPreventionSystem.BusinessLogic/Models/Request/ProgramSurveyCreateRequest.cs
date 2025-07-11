using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class ProgramSurveyCreateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
} 