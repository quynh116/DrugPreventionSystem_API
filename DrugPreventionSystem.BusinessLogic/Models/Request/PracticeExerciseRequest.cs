using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class PracticeExerciseRequest
    {
        public Guid LessonId { get; set; }
        public string? Instruction { get; set; }
        public string? AttachmentUrl { get; set; }
    }
} 