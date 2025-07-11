using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugPreventionSystem.DataAccess.Models
{
    [Table("program_survey_answer_options")]
    public class ProgramSurveyAnswerOption
    {
        [Key]
        [Column("option_id")]
        public Guid OptionId { get; set; }

        [ForeignKey("ProgramSurveyQuestion")]
        [Column("question_id")]
        public Guid QuestionId { get; set; }

        [Column("option_text")]
        public string OptionText { get; set; } = string.Empty;

        public virtual ProgramSurveyQuestion ProgramSurveyQuestion { get; set; } = null!;
    }
} 