using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface IProgramSurveyQuestionRepository
    {
        Task<List<ProgramSurveyQuestion>> GetBySurveyIdAsync(Guid surveyId);
        Task<ProgramSurveyQuestion?> GetByIdAsync(Guid questionId);
        Task AddAsync(ProgramSurveyQuestion question);
        Task UpdateAsync(ProgramSurveyQuestion question);
        Task DeleteAsync(Guid questionId);
    }
}
