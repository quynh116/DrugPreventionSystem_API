using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugPreventionSystem.DataAccess.Models
{
    [Table("program_survey_answers")]
    public class ProgramSurveyAnswer
    {
        [Key]
        [Column("answer_id")]
        public Guid AnswerId { get; set; }

        [ForeignKey("ProgramSurveyResponse")]
        [Column("response_id")]
        public Guid ResponseId { get; set; }

        [ForeignKey("ProgramSurveyQuestion")]
        [Column("question_id")]
        public Guid QuestionId { get; set; }

        [Column("answer_text")]
        public string? AnswerText { get; set; }

        [ForeignKey("ProgramSurveyAnswerOption")]
        [Column("selected_option_id")]
        public Guid? SelectedOptionId { get; set; }

        public virtual ProgramSurveyResponse ProgramSurveyResponse { get; set; } = null!;
        public virtual ProgramSurveyQuestion ProgramSurveyQuestion { get; set; } = null!;
        public virtual ProgramSurveyAnswerOption? ProgramSurveyAnswerOption { get; set; }
    }
} 