using DrugPreventionSystem.BusinessLogic.Models;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyOption;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class SurveyOptionService : ISurveyOptionService
    {
        private readonly ISurveyOptionRepository _surveyOptionRepository;

        public SurveyOptionService(ISurveyOptionRepository surveyOptionRepository)
        {
            _surveyOptionRepository = surveyOptionRepository;
        }

        public async Task<IEnumerable<SurveyOptionDTO>> GetAllAsync()
        {
            var surveyOptions = await _surveyOptionRepository.GetAllAsync();
            return surveyOptions.Select(MapToDTO);
        }

        public async Task<SurveyOptionDTO?> GetByIdAsync(Guid id)
        {
            var surveyOption = await _surveyOptionRepository.GetByIdAsync(id);
            return surveyOption != null ? MapToDTO(surveyOption) : null;
        }

        public async Task<SurveyOptionDTO> CreateAsync(SurveyOptionAddRequest surveyOptionDTO)
        {
            var surveyOption = new SurveyOption
            {
                OptionId = Guid.NewGuid(),
                QuestionId = surveyOptionDTO.QuestionId,
                OptionText = surveyOptionDTO.OptionText,
                ScoreValue = surveyOptionDTO.ScoreValue
            };

            var createdOption = await _surveyOptionRepository.CreateAsync(surveyOption);
            return MapToDTO(createdOption);
        }

        public async Task<SurveyOptionDTO> UpdateAsync(Guid id, SurveyOptionUpdateRequest surveyOptionDTO)
        {
            var existingOption = await _surveyOptionRepository.GetByIdAsync(id);
            if (existingOption == null)
                throw new KeyNotFoundException($"SurveyOption with ID {id} not found.");

            
            existingOption.OptionText = surveyOptionDTO.OptionText;
            existingOption.ScoreValue = surveyOptionDTO.ScoreValue;

            var updatedOption = await _surveyOptionRepository.UpdateAsync(existingOption);
            return MapToDTO(updatedOption);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _surveyOptionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SurveyOptionDTO>> GetByQuestionIdAsync(Guid questionId)
        {
            var surveyOptions = await _surveyOptionRepository.GetByQuestionIdAsync(questionId);
            return surveyOptions.Select(MapToDTO);
        }

        private static SurveyOptionDTO MapToDTO(SurveyOption surveyOption)
        {
            return new SurveyOptionDTO
            {
                OptionId = surveyOption.OptionId,
                QuestionId = surveyOption.QuestionId,
                OptionText = surveyOption.OptionText,
                ScoreValue = surveyOption.ScoreValue
            };
        }
    }
} 