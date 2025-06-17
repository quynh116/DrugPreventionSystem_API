using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class UserSurveyResponseResponse
    {
        public Guid ResponseId { get; set; }
        public Guid UserId { get; set; }
        public Guid SurveyId { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.Now;
        public string? RiskLevel { get; set; }
        public string? RecommendedAction { get; set; }
    }
}
