using DrugPreventionSystem.BusinessLogic.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ISurveyOptionService
    {
        Task<IEnumerable<SurveyOptionDTO>> GetAllAsync();
        Task<SurveyOptionDTO?> GetByIdAsync(Guid id);
        Task<SurveyOptionDTO> CreateAsync(SurveyOptionDTO surveyOptionDTO);
        Task<SurveyOptionDTO> UpdateAsync(Guid id, SurveyOptionDTO surveyOptionDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<SurveyOptionDTO>> GetByQuestionIdAsync(Guid questionId);
    }
} 