using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram
{
    public class ProgramSurveyAnswerDto
    {
        public Guid QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public Guid? SelectedOptionId { get; set; }
    }
    public class SubmitProgramSurveyDto
    {
        
        public List<ProgramSurveyAnswerDto> Answers { get; set; } = new List<ProgramSurveyAnswerDto>();
    }
}
