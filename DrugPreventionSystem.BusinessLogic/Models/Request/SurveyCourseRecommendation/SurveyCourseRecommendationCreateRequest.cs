using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.SurveyCourseRecommendation
{
    public class SurveyCourseRecommendationCreateRequest
    {
        public Guid SurveyId { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public string? RecommendedActionKeyword { get; set; }
        public Guid CourseId { get; set; }
        public int Priority { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
