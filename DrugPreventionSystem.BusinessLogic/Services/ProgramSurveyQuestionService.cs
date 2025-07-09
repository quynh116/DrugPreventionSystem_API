using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models;
using System.Linq;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramSurveyQuestionService : IProgramSurveyQuestionService
    {
        private readonly IProgramSurveyQuestionRepository _repository;
        public ProgramSurveyQuestionService(IProgramSurveyQuestionRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ProgramSurveyQuestion>> GetBySurveyIdAsync(Guid surveyId) => _repository.GetBySurveyIdAsync(surveyId);
        public Task<ProgramSurveyQuestion?> GetByIdAsync(Guid questionId) => _repository.GetByIdAsync(questionId);
        public Task AddAsync(ProgramSurveyQuestion question) => _repository.AddAsync(question);
        public Task UpdateAsync(ProgramSurveyQuestion question) => _repository.UpdateAsync(question);
        public Task DeleteAsync(Guid questionId) => _repository.DeleteAsync(questionId);

        public async Task<List<ProgramSurveyQuestionDto>> GetDtosBySurveyIdAsync(Guid surveyId)
        {
            var questions = await _repository.GetBySurveyIdAsync(surveyId);
            return questions.Select(MapToDto).ToList();
        }

        public async Task<ProgramSurveyQuestionDto?> GetDtoByIdAsync(Guid questionId)
        {
            var question = await _repository.GetByIdAsync(questionId);
            return question == null ? null : MapToDto(question);
        }

        private ProgramSurveyQuestionDto MapToDto(ProgramSurveyQuestion entity)
        {
            return new ProgramSurveyQuestionDto
            {
                QuestionId = entity.QuestionId,
                SurveyId = entity.SurveyId,
                QuestionText = entity.QuestionText,
                QuestionType = entity.QuestionType
            };
        }
    }
} 