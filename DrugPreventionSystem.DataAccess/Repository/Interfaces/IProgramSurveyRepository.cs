using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repository.Interfaces
{
    public interface IProgramSurveyRepository
    {
        Task<List<ProgramSurvey>> GetAllAsync();
        Task<ProgramSurvey?> GetByIdAsync(Guid surveyId);
        Task AddAsync(ProgramSurvey survey);
        Task UpdateAsync(ProgramSurvey survey);
        Task DeleteAsync(Guid surveyId);
        Task<ProgramSurvey?> GetSurveyWithDetailsAsync(Guid surveyId);
    }
}
