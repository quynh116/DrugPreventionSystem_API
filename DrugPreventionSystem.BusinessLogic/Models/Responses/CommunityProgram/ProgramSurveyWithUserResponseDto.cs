using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram
{
    public class ProgramSurveyWithUserResponseDto
    {
        public Guid SurveyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public List<ProgramSurveyQuestionDto> Questions { get; set; } = new List<ProgramSurveyQuestionDto>(); 

        
    }

    public class ProgramSurveyQuestionDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = "text";
        public List<ProgramSurveyAnswerOptionDto> AnswerOptions { get; set; } = new List<ProgramSurveyAnswerOptionDto>();
    }

    
    public class ProgramSurveyAnswerOptionDto
    {
        public Guid OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
    }

    
   
}