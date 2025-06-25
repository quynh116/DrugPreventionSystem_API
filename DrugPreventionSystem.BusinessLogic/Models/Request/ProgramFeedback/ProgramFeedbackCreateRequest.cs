using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback
{
    public class ProgramFeedbackCreateRequest
    {
        public Guid ProgramId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
    }
} 