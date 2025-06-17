using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request
{
    public class SaveSingleAnswerRequestDto
    {
        public Guid ResponseId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid OptionId { get; set; }
    }
}
