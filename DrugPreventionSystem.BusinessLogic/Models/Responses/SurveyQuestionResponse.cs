using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses
{
    public class SurveyQuestionResponse
    {
          public Guid QuestionId { get; set; }
          public Guid SurveyId { get; set; }
          public string QuestionText { get; set; } = string.Empty;
          public string QuestionType { get; set; } = string.Empty;
          public int Sequence { get; set; }
          public DateTime CreatedAt { get; set; }
          public DateTime? UpdatedAt { get; set; }
    }
}
