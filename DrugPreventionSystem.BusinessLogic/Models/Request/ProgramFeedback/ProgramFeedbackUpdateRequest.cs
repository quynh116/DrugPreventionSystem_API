using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback
{
    public class ProgramFeedbackUpdateRequest
    {
        public int Rating { get; set; }
        public string? Comments { get; set; }
    }
} 