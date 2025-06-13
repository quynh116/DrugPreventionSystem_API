using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Consultant;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ISurveyQuestionService
    {
        Task<Result<IEnumerable<SurveyQuestionResponse>>> GetAllSurveyQuestionsAsync();
        Task<Result<SurveyQuestionResponse>> GetSurveyQuestionByIdAsync(Guid surveyQuestionId);
        Task<Result<SurveyQuestionResponse>> UpdateSurveyQuestionAsync(SurveyQuestionUpdateRequest request, Guid questionId);
        Task<Result<bool>> DeleteSurveyQuestionAsync(Guid questionId);

    }
}
