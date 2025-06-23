using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.UserModuleQuizResult
{
    public class UserModuleQuizResultCreateRequest
    {
        public Guid UserId { get; set; }
        public Guid LessonId { get; set; }
        public float TotalScore { get; set; }
        public int CorrectCount { get; set; }
        public int TotalQuestions { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime TakenAt { get; set; } = DateTime.Now;
    }
}
