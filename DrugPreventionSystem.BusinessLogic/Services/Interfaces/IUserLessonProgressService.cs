using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserLessonProgress;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserLessonProgress;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserLessonProgressService
    {
        Task<Result<IEnumerable<UserLessonProgressResponse>>> GetUserLessonProgressesAsync();
        Task<Result<UserLessonProgressResponse?>> GetUserLessonProgressByIdAsync(Guid progressId);
        Task<Result<IEnumerable<UserLessonProgressResponse>>> GetUserLessonProgressByUserIdAsync(Guid userId);
        Task<Result<IEnumerable<UserLessonProgressResponse>>> GetUserLessonProgressByLessonIdAsync(Guid lessonId);
        Task<Result<UserLessonProgressResponse>> AddUserLessonProgressAsync(UserLessonProgressCreateRequest request);
        Task<Result<UserLessonProgressResponse>> UpdateUserLessonProgressAsync(Guid progressId, UserLessonProgressUpdateRequest request);
        Task<Result<UserLessonProgressResponse>> DeleteUserLessonProgressAsync(Guid progressId);
    }
}
