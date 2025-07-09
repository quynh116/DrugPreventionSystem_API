using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IProgramSurveyAnswerOptionService
    {
        Task<List<ProgramSurveyAnswerOptionDto>> GetDtosByQuestionIdAsync(Guid questionId);
        Task<ProgramSurveyAnswerOptionDto?> GetDtoByIdAsync(Guid optionId);
        Task AddAsync(ProgramSurveyAnswerOption option);
        Task UpdateAsync(ProgramSurveyAnswerOption option);
        Task DeleteAsync(Guid optionId);
        Task<ProgramSurveyAnswerOption?> GetEntityByIdAsync(Guid optionId);
    }
} 