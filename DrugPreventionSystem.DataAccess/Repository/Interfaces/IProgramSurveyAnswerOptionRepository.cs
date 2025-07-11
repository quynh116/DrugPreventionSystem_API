using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface IProgramSurveyAnswerOptionRepository
    {
        Task<List<ProgramSurveyAnswerOption>> GetByQuestionIdAsync(Guid questionId);
        Task<ProgramSurveyAnswerOption?> GetByIdAsync(Guid optionId);
        Task AddAsync(ProgramSurveyAnswerOption option);
        Task UpdateAsync(ProgramSurveyAnswerOption option);
        Task DeleteAsync(Guid optionId);
    }
}
