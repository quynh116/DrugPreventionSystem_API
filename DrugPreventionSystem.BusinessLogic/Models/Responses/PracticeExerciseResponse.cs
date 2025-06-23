using System;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class PracticeExerciseResponse
    {
        public Guid ExerciseId { get; set; }
        public Guid LessonId { get; set; }
        public string? Instruction { get; set; }
        public string? AttachmentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 