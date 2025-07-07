using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugPreventionSystem.DataAccess.Models
{
    [Table("program_surveys")]
    public class ProgramSurvey
    {
        [Key]
        [Column("survey_id")]
        public Guid SurveyId { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        public virtual ICollection<ProgramSurveyQuestion> Questions { get; set; } = new List<ProgramSurveyQuestion>();
        public virtual ICollection<CommunityProgram> Programs { get; set; } = new List<CommunityProgram>();
    }
} 