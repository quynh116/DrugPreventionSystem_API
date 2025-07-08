using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using DrugPreventionSystem.BusinessLogic.Models;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramSurveyAnswerOptionService : IProgramSurveyAnswerOptionService
    {
        private readonly IProgramSurveyAnswerOptionRepository _repository;
        public ProgramSurveyAnswerOptionService(IProgramSurveyAnswerOptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProgramSurveyAnswerOptionDto>> GetDtosByQuestionIdAsync(Guid questionId)
        {
            var options = await _repository.GetByQuestionIdAsync(questionId);
            return options.Select(MapToDto).ToList();
        }

        public async Task<ProgramSurveyAnswerOptionDto?> GetDtoByIdAsync(Guid optionId)
        {
            var option = await _repository.GetByIdAsync(optionId);
            return option == null ? null : MapToDto(option);
        }

        public Task AddAsync(ProgramSurveyAnswerOption option) => _repository.AddAsync(option);
        public Task UpdateAsync(ProgramSurveyAnswerOption option) => _repository.UpdateAsync(option);
        public Task DeleteAsync(Guid optionId) => _repository.DeleteAsync(optionId);
        public Task<ProgramSurveyAnswerOption?> GetEntityByIdAsync(Guid optionId) => _repository.GetByIdAsync(optionId);

        private ProgramSurveyAnswerOptionDto MapToDto(ProgramSurveyAnswerOption entity)
        {
            return new ProgramSurveyAnswerOptionDto
            {
                OptionId = entity.OptionId,
                QuestionId = entity.QuestionId,
                OptionText = entity.OptionText
            };
        }
    }
} 