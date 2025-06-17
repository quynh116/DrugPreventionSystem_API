using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyResponse
{
    public class UserSurveyResponseUpdateRequest
    {
        public DateTime TakenAt { get; set; } = DateTime.Now;
        public string? RiskLevel { get; set; }
        public string? RecommendedAction { get; set; }
    }
}
