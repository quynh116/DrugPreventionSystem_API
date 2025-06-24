using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes
{
    public interface IQuizQuestionService
    {
        Task<Result<QuizQuestionResponse>> CreateAsync(QuizQuestionCreateRequest request);
        Task<Result<IEnumerable<QuizQuestionResponse>>> GetAllAsync();
        Task<Result<QuizQuestionResponse>> GetByIdAsync(Guid id);
        Task<Result<QuizQuestionResponse>> UpdateAsync(QuizQuestionUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
    }

}
