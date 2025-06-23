using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IPracticeExerciseService
    {
        Task<Result<PracticeExerciseResponse>> AddAsync(PracticeExerciseRequest request);
        Task<Result<IEnumerable<PracticeExerciseResponse>>> GetAllAsync();
        Task<Result<PracticeExerciseResponse>> GetByIdAsync(Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<PracticeExerciseResponse>> UpdateAsync(Guid id, PracticeExerciseRequest request);
        Task<Result<IEnumerable<PracticeExerciseResponse>>> GetByLessonIdAsync(Guid lessonId);
    }
} 