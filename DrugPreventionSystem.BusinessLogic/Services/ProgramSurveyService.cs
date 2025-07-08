using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models;
using System.Linq;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramSurveyService
    {
        private readonly ProgramSurveyRepository _repository;
        public ProgramSurveyService(ProgramSurveyRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ProgramSurvey>> GetAllAsync() => _repository.GetAllAsync();
        public Task<ProgramSurvey?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task AddAsync(ProgramSurvey survey) => _repository.AddAsync(survey);
        public Task UpdateAsync(ProgramSurvey survey) => _repository.UpdateAsync(survey);
        public Task DeleteAsync(Guid id) => _repository.DeleteAsync(id);

        public async Task<List<ProgramSurveyDto>> GetAllDtoAsync()
        {
            var surveys = await _repository.GetAllAsync();
            return surveys.Select(MapToDto).ToList();
        }

        public async Task<ProgramSurveyDto?> GetDtoByIdAsync(Guid id)
        {
            var survey = await _repository.GetByIdAsync(id);
            return survey == null ? null : MapToDto(survey);
        }

        private ProgramSurveyDto MapToDto(ProgramSurvey entity)
        {
            return new ProgramSurveyDto
            {
                SurveyId = entity.SurveyId,
                Title = entity.Title,
                Description = entity.Description
            };
        }
    }
} 