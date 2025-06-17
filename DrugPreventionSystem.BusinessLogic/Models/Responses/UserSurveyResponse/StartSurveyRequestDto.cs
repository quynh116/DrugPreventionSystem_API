using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyResponse
{
    public class StartSurveyResponseDto
    {
        public Guid ResponseId { get; set; }
        public Guid UserId { get; set; }
        public Guid SurveyId { get; set; }
        public DateTime TakenAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
