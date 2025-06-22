using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserQuizAnswer;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserLessonProgress;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserQuizAnswer;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserQuizAnswerService
    {
        Task<Result<IEnumerable<UserQuizAnswerResponse>>> GetUserQuizAnswersAsync();
        Task<Result<UserQuizAnswerResponse?>> GetUserQuizAnswerByIdAsync(Guid userQuizAnswerId);
        Task<Result<IEnumerable<UserQuizAnswerResponse>>> GetUserQuizAnswersByUserIdAsync(Guid userId);
        Task<Result<IEnumerable<UserQuizAnswerResponse>>> GetUserQuizAnswersByQuestionIdAsync(Guid questionId);
        Task<Result<UserQuizAnswerResponse>> AddUserQuizAnswerAsync(UserQuizAnswerCreateRequest request);
        Task<Result<UserQuizAnswerResponse>> UpdateUserQuizAnswerAsync(Guid userQuizAnswerId, UserQuizAnswerUpdateRequest request);
        Task<Result<UserQuizAnswerResponse>> DeleteUserQuizAnswerAsync(Guid userQuizAnswerId);
    }
}
