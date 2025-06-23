using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserModuleQuizResult;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserModuleQuizResult;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserModuleQuizResultService
    {
        Task<Result<IEnumerable<UserModuleQuizResultResponse>>> GetUserModuleQuizResultsAsync();
        Task<Result<UserModuleQuizResultResponse?>> GetUserModuleQuizResultByIdAsync(Guid resultId);
        Task<Result<IEnumerable<UserModuleQuizResultResponse>>> GetUserModuleQuizResultsByUserIdAsync(Guid userId);
        Task<Result<IEnumerable<UserModuleQuizResultResponse>>> GetUserModuleQuizResultsByLessonIdAsync(Guid lessonId);
        Task<Result<UserModuleQuizResultResponse>> AddUserModuleQuizResultAsync(UserModuleQuizResultCreateRequest request);
        Task<Result<UserModuleQuizResultResponse>> UpdateUserModuleQuizResultAsync(Guid resultId, UserModuleQuizResultUpdateRequest request);
        Task<Result<UserModuleQuizResultResponse>> DeleteUserModuleQuizResultAsync(Guid resultId);
    }
}
