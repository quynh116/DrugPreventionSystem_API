using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.SurveyOption
{
    public class SurveyOptionUpdateRequest
    {
        [MaxLength(500)]
        public string OptionText { get; set; } = string.Empty;

        public int? ScoreValue { get; set; }
    }
}
