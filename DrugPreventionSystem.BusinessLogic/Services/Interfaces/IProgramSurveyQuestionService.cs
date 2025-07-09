using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IProgramSurveyQuestionService
    {
        Task<List<ProgramSurveyQuestion>> GetBySurveyIdAsync(Guid surveyId);
        Task<ProgramSurveyQuestion?> GetByIdAsync(Guid questionId);
        Task AddAsync(ProgramSurveyQuestion question);
        Task UpdateAsync(ProgramSurveyQuestion question);
        Task DeleteAsync(Guid questionId);
        Task<List<ProgramSurveyQuestionDto>> GetDtosBySurveyIdAsync(Guid surveyId);
        Task<ProgramSurveyQuestionDto?> GetDtoByIdAsync(Guid questionId);
    }
} 