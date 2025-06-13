using DrugPreventionSystem.BusinessLogic.Models;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyOption;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ISurveyOptionService
    {
        Task<IEnumerable<SurveyOptionDTO>> GetAllAsync();
        Task<SurveyOptionDTO?> GetByIdAsync(Guid id);
        Task<SurveyOptionDTO> CreateAsync(SurveyOptionAddRequest surveyOptionDTO);
        Task<SurveyOptionDTO> UpdateAsync(Guid id, SurveyOptionUpdateRequest surveyOptionDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<SurveyOptionDTO>> GetByQuestionIdAsync(Guid questionId);
    }
} 