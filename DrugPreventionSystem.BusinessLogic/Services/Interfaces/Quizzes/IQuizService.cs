using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.Quiz;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes
{
    public interface IQuizService
    {
        Task<Result<QuizResponse>> CreateAsync(QuizCreateRequest request);
        Task<Result<IEnumerable<QuizResponse>>> GetAllAsync();
        Task<Result<QuizResponse>> GetByIdAsync(Guid id);
        Task<Result<QuizResponse>> UpdateAsync(QuizUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<QuizFullEditDto>> GetQuizByLessonIdForEditAsync(Guid lessonId);
    }

}
