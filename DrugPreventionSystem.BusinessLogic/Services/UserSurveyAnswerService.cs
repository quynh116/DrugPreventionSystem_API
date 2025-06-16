using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyAnswer;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserSurveyAnswer;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserSurveyAnswerService : IUserSurveyAnswerService
    {
        private readonly IUserSurveyAnswerRepository _userSurveyAnswerRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyOptionRepository _surveyOptionRepository;
        private readonly IUserService _userService;

        public UserSurveyAnswerService(IUserSurveyAnswerRepository userSurveyAnswerRepository, 
                                        ISurveyQuestionRepository surveyQuestionRepository,
                                        ISurveyOptionRepository surveyOptionRepository,
                                        IUserService userService) { 
            _userSurveyAnswerRepository = userSurveyAnswerRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyOptionRepository = surveyOptionRepository;
            _userService = userService;
        }
        private UserSurveyAnswerResponse MapToUserSurveyAnswerResponse(UserSurveyAnswer userSurveyAnswer)
        {
            return new UserSurveyAnswerResponse
            {
                AnswerId = userSurveyAnswer.AnswerId,
                ResponseId = userSurveyAnswer.ResponseId,
                QuestionId = userSurveyAnswer.QuestionId,
                OptionId = userSurveyAnswer.OptionId,
                AnswerText = userSurveyAnswer.AnswerText,
                AnsweredAt = userSurveyAnswer.AnsweredAt
            };
        }

        public async Task<Result<UserSurveyAnswerResponse>> AddUserSurveyAnswerAsync(UserSurveyAnswerCreateRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.AnswerText))
                {
                    return Result<UserSurveyAnswerResponse>.Invalid("Answer text is required");
                }
                var surveyOption = await _surveyOptionRepository.GetByIdAsync(request.OptionId);
                if (surveyOption == null)
                {
                    return Result<UserSurveyAnswerResponse>.Invalid("This option is not exist");
                }
                var surveyQuestion = await _surveyQuestionRepository.GetSurveyQuestionByIdAsync(request.QuestionId);
                if (surveyQuestion == null)
                {
                    return Result<UserSurveyAnswerResponse>.Invalid("This question is not exist");
                }
                var userSurveyAnswer = new UserSurveyAnswer
                {
                    ResponseId = request.ResponseId,
                    QuestionId = request.QuestionId,
                    OptionId = request.OptionId,
                    AnswerText = request.AnswerText,
                    AnsweredAt = request.AnsweredAt
                };
                await _userSurveyAnswerRepository.AddNewUserSurveyAnswer(userSurveyAnswer);
                return Result<UserSurveyAnswerResponse>.Success(MapToUserSurveyAnswerResponse(userSurveyAnswer), "User survey answer added successfully");
            }
            catch (Exception ex)
            {
                return Result<UserSurveyAnswerResponse>.Error($"Error adding answer: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteUserSurveyAnswerAsync(Guid id)
        {
            try
            {
                var answer = await _userSurveyAnswerRepository.GetUserSurveyAnswerByIdAsync(id);
                if(answer == null)
                {
                    return Result<bool>.NotFound($"Cannot find user survey answer with id: {id}");
                }
                await _userSurveyAnswerRepository.DeleteUserSurveyAnswerByIdAsync(id);
                return Result<bool>.Success(true, "User survey answer deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting user survey answer: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserSurveyAnswerResponse>>> GetAllUserSurveyAnswersAsync()
        {
            try
            {
                var result = await _userSurveyAnswerRepository.GetAllUserSurveyAnswersAsync();
                if (result == null || !result.Any())
                {
                    return Result<IEnumerable<UserSurveyAnswerResponse>>.NotFound("No user survey answers found");
                }
                return Result<IEnumerable<UserSurveyAnswerResponse>>.Success(
                    result.Select(s => MapToUserSurveyAnswerResponse(s)).ToList(),
                    "User survey answers retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserSurveyAnswerResponse>>.Error($"Error retrieving user survey answers: {ex.Message}");
            }
        }

        public async Task<Result<UserSurveyAnswerResponse>> GetUserSurveyAnswerByIdAsync(Guid id)
        {
            try
            {
                var result = await _userSurveyAnswerRepository.GetUserSurveyAnswerByIdAsync(id);
                if (result == null)
                {
                    return Result<UserSurveyAnswerResponse>.NotFound($"Cannot find user survey answer with id: {id}");
                }
                return Result<UserSurveyAnswerResponse>.Success(
                    MapToUserSurveyAnswerResponse(result),
                    "User survey answer retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                return Result<UserSurveyAnswerResponse>.Error($"Error retrieving user survey answer: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserSurveyAnswerResponse>>> GetUserSurveyAnswersByUserIdAsync(Guid userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return Result<IEnumerable<UserSurveyAnswerResponse>>.NotFound($"Cannot find user with id: {userId}");
                }
                var result = await _userSurveyAnswerRepository.GetUserSurveyAnswerByUserIdAsync(userId);
                if(result == null || !result.Any())
                {
                    return Result<IEnumerable<UserSurveyAnswerResponse>>.NotFound("No user survey answers found for this user");
                }
                return Result<IEnumerable<UserSurveyAnswerResponse>>.Success(
                    result.Select(s => MapToUserSurveyAnswerResponse(s)).ToList(),
                    "User survey answers retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserSurveyAnswerResponse>>.Error($"Error retrieving user survey answers by user id: {ex.Message}");
            }
        }

        public async Task<Result<UserSurveyAnswerResponse>> UpdateUserSurveyAnswerAsync(Guid id, UserSurveyAnswerUpdateRequest request)
        {
            try
            {
                var answer = await _userSurveyAnswerRepository.GetUserSurveyAnswerByIdAsync(id);
                if (answer == null)
                {
                    return Result<UserSurveyAnswerResponse>.NotFound($"Cannot find user survey answer with id: {id}");
                }
                if (string.IsNullOrEmpty(request.AnswerText))
                {
                    return Result<UserSurveyAnswerResponse>.Invalid("Answer text is required");
                }

                var surveyQuestion = await _surveyQuestionRepository.GetSurveyQuestionByIdAsync(answer.QuestionId);
                if (surveyQuestion == null)
                {
                    return Result<UserSurveyAnswerResponse>.Invalid("This question does not exist");
                }

                var surveyOption = await _surveyOptionRepository.GetByIdAsync(request.OptionId);
                if (surveyOption == null)
                {
                    return Result<UserSurveyAnswerResponse>.Invalid("This option does not exist");
                }
                answer.AnswerText = request.AnswerText;
                answer.OptionId = request.OptionId;
                await _surveyOptionRepository.UpdateAsync(surveyOption);
                return Result<UserSurveyAnswerResponse>.Success(
                    MapToUserSurveyAnswerResponse(answer),
                    "User survey answer updated successfully"
                );
            }
            catch (Exception ex)
            {
                return Result<UserSurveyAnswerResponse>.Error($"Error updating user survey answer: {ex.Message}");
            }
        }
    }
}
