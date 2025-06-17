using System.ComponentModel.DataAnnotations;

namespace DrugPreventionSystem.BusinessLogic.Models
{
    public class SurveyOptionDTO
    {
        public Guid? OptionId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        [MaxLength(500)]
        public string OptionText { get; set; } = string.Empty;

        public int? ScoreValue { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}