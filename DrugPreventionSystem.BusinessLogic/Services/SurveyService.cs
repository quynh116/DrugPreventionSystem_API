using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models;
using DrugPreventionSystem.BusinessLogic.Models.Request.Survey;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyOptionRepository _surveyOptionRepository;

        public SurveyService(ISurveyRepository surveyRepository, ISurveyQuestionRepository surveyQuestionRepository, ISurveyOptionRepository surveyOptionRepository)
        {
            _surveyRepository = surveyRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyOptionRepository = surveyOptionRepository;
        }

        private SurveyResponse MapToSurveyReponse(Survey survey)
        {
            if (survey == null) return null;
            return new SurveyResponse
            {
                SurveyId = survey.SurveyId,
                Name = survey.Name,
                Description = survey.Description,
                CreatedAt = survey.CreatedAt,
                UpdatedAt = survey.UpdatedAt
            };
        }

        private SurveyQuestionResponse MapToSurveyQuestionResponse(SurveyQuestion question)
        {
            if (question == null) return null;
            return new SurveyQuestionResponse
            {
                QuestionId = question.QuestionId,
                SurveyId = question.SurveyId,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                Sequence = question.Sequence,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                Options = question.SurveyOptions? // Map options if available
                                .Select(o => new SurveyOptionDTO
                                {
                                    OptionId = o.OptionId,
                                    QuestionId = o.QuestionId,
                                    OptionText = o.OptionText,
                                    ScoreValue = o.ScoreValue,
                                    CreatedAt = o.CreatedAt,
                                    UpdatedAt = o.UpdatedAt
                                })
                                .ToList() ?? new List<SurveyOptionDTO>()
            };
        }

        public async Task<Result<bool>> DeleteSurveyByIdAsync(Guid id)
        {
            try
            {
                var survey = await _surveyRepository.GetSurveyByIdAsync(id);
                if (survey == null) return Result<bool>.NotFound($"Cannot find survey with id: {id}");

                await _surveyRepository.DeleteSurveyByIdAsync(id);
                return Result<bool>.Success(true, "Survey Deleted Successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error Deleting Survey: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<SurveyResponse>>> GetAllSurveyAsync()
        {
            try
            {
                var surveys = await _surveyRepository.GetAllSurveyAsync();
                if (surveys == null) return Result<IEnumerable<SurveyResponse>>.NotFound("Empty survey list");
                var surveyResponse = surveys.Select(s => MapToSurveyReponse(s)).ToList();
                return Result<IEnumerable<SurveyResponse>>.Success(surveyResponse);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SurveyResponse>>.Error($"Error retrieving surveys: {ex.Message}");
            }
        }

        public async Task<Result<SurveyResponse>> GetSurveyByIdAsync(Guid id)
        {
            try
            {
                var survey = await _surveyRepository.GetSurveyByIdAsync(id);
                if (survey == null) return Result<SurveyResponse>.NotFound($"Cannot find survey with id: {id}");
                return Result<SurveyResponse>.Success(MapToSurveyReponse(survey));
            }
            catch (Exception ex)
            {
                return Result<SurveyResponse>.Error($"Error retrieving survey: {ex.Message}");
            }
        }

        public async Task<Result<SurveyResponse>> UpdateSurveyAsync(Guid id, SurveyUpdateRequest request)
        {
            try
            {
                var survey = await _surveyRepository.GetSurveyByIdAsync(id);
                if (survey == null) return Result<SurveyResponse>.NotFound($"Cannot find survey with id: {id}");
                if(!string.IsNullOrEmpty(request.Name) && request.Name != survey.Name)
                {
                    var existingSurvey = await _surveyRepository.GetSurveyByNameAsync(request.Name);
                    if (existingSurvey != null && existingSurvey.SurveyId != survey.SurveyId)
                    {
                        return Result<SurveyResponse>.Duplicated("Survey name already exist.");
                    }
                    survey.Name = request.Name;
                }
                if(!string.IsNullOrEmpty(request.Description) && request.Description != survey.Description)
                {
                    survey.Description = request.Description;
                }
                survey.UpdatedAt = DateTime.Now;
                await _surveyRepository.UpdateSurveyAsync(survey);
                return Result<SurveyResponse>.Success(MapToSurveyReponse(survey), "Update successfully");
            }
            catch (Exception ex)
            {
                return Result<SurveyResponse>.Error($"Error updating survey: {ex.Message}");
            }
        }

        public async Task<Result<SurveyResponse>> AddNewSurveyAsync(SurveyCreateRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Result<SurveyResponse>.Invalid("Survey name is required.");
                }

                if (await _surveyRepository.GetSurveyByNameAsync(request.Name) != null)
                {
                    return Result<SurveyResponse>.Duplicated("Survey name already exists.");
                }

                var now = DateTime.Now;
                var survey = new Survey
                {
                    SurveyId = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = now,
                    UpdatedAt = now
                };

                var addedSurvey = await _surveyRepository.AddNewSurvey(survey);

                return Result<SurveyResponse>.Success(MapToSurveyReponse(addedSurvey), "Added successfully");
            }
            catch (Exception ex)
            {
                return Result<SurveyResponse>.Error($"Error adding survey: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<SurveyQuestionResponse>>> GetSurveyQuestionsWithAllDetailsAsync(Guid surveyId)
        {
            try
            {
                var survey = await _surveyRepository.GetSurveyByIdAsync(surveyId);
                if (survey == null)
                {
                    return Result<IEnumerable<SurveyQuestionResponse>>.NotFound("Không tìm thấy khảo sát.");
                }

                var questions = await _surveyQuestionRepository.GetSurveyQuestionsWithAllDetailsBySurveyIdAsync(surveyId);
                var questionResponses = questions.Select(q => MapToSurveyQuestionResponse(q)).ToList();

                if (!questionResponses.Any())
                {
                    return Result<IEnumerable<SurveyQuestionResponse>>.NotFound($"Không tìm thấy câu hỏi nào cho khảo sát ID {surveyId}.");
                }

                return Result<IEnumerable<SurveyQuestionResponse>>.Success(questionResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SurveyQuestionResponse>>.Error($"Lỗi khi lấy câu hỏi khảo sát: {ex.Message}");
            }
        }
    }
}
