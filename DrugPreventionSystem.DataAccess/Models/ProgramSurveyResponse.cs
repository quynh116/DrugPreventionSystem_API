using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugPreventionSystem.DataAccess.Models
{
    [Table("program_survey_responses")]
    public class ProgramSurveyResponse
    {
        [Key]
        [Column("response_id")]
        public Guid ResponseId { get; set; }

        [ForeignKey("ProgramSurvey")]
        [Column("survey_id")]
        public Guid SurveyId { get; set; }

        [ForeignKey("CommunityProgram")]
        [Column("program_id")]
        public Guid ProgramId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("submitted_at")]
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public virtual ProgramSurvey ProgramSurvey { get; set; } = null!;
        public virtual CommunityProgram CommunityProgram { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ProgramSurveyAnswer> Answers { get; set; } = new List<ProgramSurveyAnswer>();
    }
} 