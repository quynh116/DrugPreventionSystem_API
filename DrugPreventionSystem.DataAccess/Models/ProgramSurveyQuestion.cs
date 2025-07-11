using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugPreventionSystem.DataAccess.Models
{
    [Table("program_survey_questions")]
    public class ProgramSurveyQuestion
    {
        [Key]
        [Column("question_id")]
        public Guid QuestionId { get; set; }

        [ForeignKey("ProgramSurvey")]
        [Column("survey_id")]
        public Guid SurveyId { get; set; }

        [Column("question_text")]
        public string QuestionText { get; set; } = string.Empty;

        [Column("question_type")]
        public string QuestionType { get; set; } = "text"; // "text" | "multiple_choice"

        public virtual ProgramSurvey ProgramSurvey { get; set; } = null!;
        public virtual ICollection<ProgramSurveyAnswerOption> AnswerOptions { get; set; } = new List<ProgramSurveyAnswerOption>();
    }
} 