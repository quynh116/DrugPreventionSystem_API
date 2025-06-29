using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.ProgramFeedback
{
    public class ProgramFeedbackResponse
    {
        public Guid FeedbackId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
} 