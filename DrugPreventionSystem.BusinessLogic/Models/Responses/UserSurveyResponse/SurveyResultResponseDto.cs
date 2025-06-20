using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyResponse
{
    public class SurveyResultResponseDto
    {
        public Guid ResponseId { get; set; }
        public Guid UserId { get; set; }
        public Guid SurveyId { get; set; }
        public DateTime TakenAt { get; set; }
        public int TotalScore { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public List<string> RecommendedActions { get; set; } = new List<string>();
        public string Disclaimer { get; set; } = "Lưu ý: Kết quả này chỉ mang tính chất tham khảo và không thay thế cho chẩn đoán chuyên nghiệp. Nếu bạn lo lắng về việc sử dụng chất gây nghiện, vui lòng tham khảo ý kiến của chuyên viên tư vấn.";
        public List<string> ScoreInterpretation { get; set; } = new List<string>();
    }
}
