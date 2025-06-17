using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyAnswer;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyAnswer;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserSurveyAnswerService
    {
        Task<Result<IEnumerable<UserSurveyAnswerResponse>>> GetAllUserSurveyAnswersAsync();
        Task<Result<UserSurveyAnswerResponse>> GetUserSurveyAnswerByIdAsync(Guid id);
        Task<Result<IEnumerable<UserSurveyAnswerResponse>>> GetUserSurveyAnswersByUserIdAsync(Guid userId);
        Task<Result<UserSurveyAnswerResponse>> AddUserSurveyAnswerAsync(UserSurveyAnswerCreateRequest request);
        Task<Result<UserSurveyAnswerResponse>> UpdateUserSurveyAnswerAsync(Guid id, UserSurveyAnswerUpdateRequest request);
        Task<Result<bool>> DeleteUserSurveyAnswerAsync(Guid id);
    }
}
