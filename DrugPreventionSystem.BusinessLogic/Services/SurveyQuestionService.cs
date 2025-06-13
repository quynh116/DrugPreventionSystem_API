using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Consultant;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ProvideToken _provideToken;
        public SurveyQuestionService(ISurveyQuestionRepository surveyQuestionRepositories, ProvideToken provideToken)
        {
            _surveyQuestionRepository = surveyQuestionRepositories;
            _provideToken = provideToken;
        }
        private SurveyQuestionResponse MapToResponse(SurveyQuestion question)
        {
            if (question == null) { 
                return null;
            }
            return new SurveyQuestionResponse
            {
                QuestionId = question.QuestionId,
                SurveyId = question.SurveyId,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                Sequence = question.Sequence,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt
            };


        }
        public async Task<Result<IEnumerable<SurveyQuestionResponse>>> GetAllSurveyQuestionsAsync()
        {
            try
            {
                var surveyQuestions = await _surveyQuestionRepository.GetAllSurveyQuestionsAsync();
                var surveyQuestionResponses = surveyQuestions.Select(c => MapToResponse(c)).ToList();
                return Result<IEnumerable<SurveyQuestionResponse>>.Success(surveyQuestionResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SurveyQuestionResponse>>.Error($"Survey question is null here: {ex.Message}");
            }
        }
        public async Task<Result<SurveyQuestionResponse>> GetSurveyQuestionByIdAsync(Guid surveyQuestionId)
        {
            try
            {
                var surveyQuestion = await _surveyQuestionRepository.GetSurveyQuestionByIdAsync(surveyQuestionId);
                if (surveyQuestion == null)
                {
                    return Result<SurveyQuestionResponse>.NotFound($"Consultant with ID {surveyQuestionId} not found.");
                }
                return Result<SurveyQuestionResponse>.Success(MapToResponse(surveyQuestion));
            }
            catch (Exception ex)
            {
                return Result<SurveyQuestionResponse>.Error($"Error retrieving consultant: {ex.Message}");
            }
        }

        public async Task<Result<SurveyQuestionResponse>> UpdateSurveyQuestionAsync(SurveyQuestionUpdateRequest request, Guid questionId)
        {
            try
            {
                var question = await _surveyQuestionRepository.GetSurveyQuestionByIdAsync(questionId);
                if (question == null)
                {
                    return Result<SurveyQuestionResponse>.NotFound($"Survey question with ID {questionId} not found.");
                }

                // Cập nhật tất cả các trường từ request
                
                question.QuestionText = request.QuestionText;
                question.QuestionType = request.QuestionType;
                question.Sequence = request.Sequence;
                question.UpdatedAt = DateTime.Now;

                await _surveyQuestionRepository.UpdateSurveyQuestion(question);

                return Result<SurveyQuestionResponse>.Success(MapToResponse(question), "Survey question updated successfully.");
            }
            catch (Exception ex)
            {
                return Result<SurveyQuestionResponse>.Error($"Error updating survey question: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteSurveyQuestionAsync(Guid questionId)
        {
            try
            {
                var question = await _surveyQuestionRepository.GetSurveyQuestionByIdAsync(questionId);
                if (question == null)
                {
                    return Result<bool>.NotFound($"Survey question with ID {questionId} not found.");
                }

                await _surveyQuestionRepository.DeleteSurveyQuestionAsync(questionId);
                return Result<bool>.Success(true, "Survey question deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting survey question: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<SurveyQuestionResponse>>> GetSurveyQuestionsBySurveyIdAsync(Guid surveyId)
        {
            try
            {
                var surveyQuestions = await _surveyQuestionRepository.GetSurveyQuestionsBySurveyIdAsync(surveyId);

                if (surveyQuestions == null || !surveyQuestions.Any())
                {
                    return Result<IEnumerable<SurveyQuestionResponse>>.NotFound($"No survey questions found for SurveyId {surveyId}");
                }

                var surveyQuestionResponses = surveyQuestions.Select(q => MapToResponse(q)).ToList();

                return Result<IEnumerable<SurveyQuestionResponse>>.Success(surveyQuestionResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SurveyQuestionResponse>>.Error($"Error retrieving survey questions: {ex.Message}");
            }
        }
    }
}
