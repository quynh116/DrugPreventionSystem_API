using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.Survey
{
    public class StartSurveyRequestDto
    {
        public Guid UserId { get; set; }
        public Guid SurveyId { get; set; }
    }
}
