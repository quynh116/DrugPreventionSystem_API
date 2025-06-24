using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizOption;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes
{
    public interface IQuizOptionService
    {
        Task<Result<QuizOptionResponse>> CreateAsync(QuizOptionCreateRequest request);
        Task<Result<IEnumerable<QuizOptionResponse>>> GetAllAsync();
        Task<Result<QuizOptionResponse>> GetByIdAsync(Guid id);
        Task<Result<QuizOptionResponse>> UpdateAsync(QuizOptionUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
    }


}
