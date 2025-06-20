using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Survey;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<Result<SurveyResponse>> AddNewSurveyAsync(SurveyCreateRequest request);
        Task<Result<IEnumerable<SurveyResponse>>> GetAllSurveyAsync();
        Task<Result<SurveyResponse>> GetSurveyByIdAsync(Guid id);
        Task<Result<bool>> DeleteSurveyByIdAsync(Guid id);
        Task<Result<SurveyResponse>> UpdateSurveyAsync(Guid id, SurveyUpdateRequest request);
        Task<Result<IEnumerable<SurveyQuestionResponse>>> GetSurveyQuestionsWithAllDetailsAsync(Guid surveyId);

    }
}
