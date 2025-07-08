using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface IProgramSurveyResponseRepository
    {
        Task<List<ProgramSurveyResponse>> GetBySurveyIdAsync(Guid surveyId);
        Task<ProgramSurveyResponse?> GetByIdAsync(Guid responseId);
        Task AddAsync(ProgramSurveyResponse response);
        Task UpdateAsync(ProgramSurveyResponse response);
        Task DeleteAsync(Guid responseId);
    }
}
