using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IProgramSurveyService
    {
        Task<List<ProgramSurvey>> GetAllAsync();
        Task<ProgramSurvey?> GetByIdAsync(Guid id);
        Task AddAsync(ProgramSurvey survey);
        Task UpdateAsync(ProgramSurvey survey);
        Task DeleteAsync(Guid id);
        Task<List<ProgramSurveyDto>> GetAllDtoAsync();
        Task<ProgramSurveyDto?> GetDtoByIdAsync(Guid id);
    }
} 