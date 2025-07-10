using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models;
using System.Linq;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramSurveyService : IProgramSurveyService
    {
        private readonly ProgramSurveyRepository _repository;
        private readonly ICommunityProgramRepository _programRepository;

        public ProgramSurveyService(ProgramSurveyRepository repository, ICommunityProgramRepository programRepository)
        {
            _repository = repository;
            _programRepository = programRepository;
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

        public async Task<ProgramSurveyDto> CreateProgramSurveyAndLinkToProgramAsync(ProgramSurveyCreateRequest request)
        {
            
            var program = await _programRepository.GetProgramByIdAsync(request.ProgramId);
            if (program == null)
            {
                throw new ArgumentException($"Chương trình với ID '{request.ProgramId}' không tìm thấy.");
            }

            
            if (program.SurveyId.HasValue)
            {
                throw new InvalidOperationException($"Chương trình '{program.Title}' đã có một khảo sát được liên kết.");
            }

            
            var newSurvey = new ProgramSurvey
            {
                SurveyId = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description
                
            };

            
            await _repository.AddAsync(newSurvey);

            
            program.SurveyId = newSurvey.SurveyId;
            
            program.UpdatedAt = DateTime.UtcNow; 

            await _programRepository.UpdateCommunityProgramAsync(program); 

            
            return new ProgramSurveyDto
            {
                SurveyId = newSurvey.SurveyId,
                Title = newSurvey.Title,
                Description = newSurvey.Description
            };
        }
    }
} 