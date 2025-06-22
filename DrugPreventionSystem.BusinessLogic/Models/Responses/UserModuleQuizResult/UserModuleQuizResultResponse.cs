using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserModuleQuizResult
{
    public class UserModuleQuizResultResponse
    {
        public Guid ResultId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid LessonId { get; set; }
        public float TotalScore { get; set; }
        public int CorrectCount { get; set; }
        public int TotalQuestions { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime TakenAt { get; set; } = DateTime.Now;
    }
}
