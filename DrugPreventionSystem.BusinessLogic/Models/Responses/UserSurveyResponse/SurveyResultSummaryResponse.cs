using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyResponse
{
    public class SurveyResultSummaryResponse
    {
        public Guid ResponseId { get; set; }
        public Guid SurveyId { get; set; }
        public string SurveyName { get; set; } = string.Empty; // Tên bài đánh giá
        public DateTime TakenAt { get; set; } // Thời gian làm bài
        public int TotalScore { get; set; } // Điểm số
        public string RiskLevel { get; set; } = string.Empty; // Mức độ nguy cơ
        public List<string> RecommendedActions { get; set; } = new List<string>();
    }
}
