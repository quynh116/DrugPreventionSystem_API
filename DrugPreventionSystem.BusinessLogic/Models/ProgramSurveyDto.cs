using System;

namespace DrugPreventionSystem.BusinessLogic.Models
{
    public class ProgramSurveyDto
    {
        public Guid SurveyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
} 