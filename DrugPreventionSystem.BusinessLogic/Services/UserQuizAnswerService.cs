using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserQuizAnswer;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserQuizAnswer;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repositories;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserQuizAnswerService : IUserQuizAnswerService
    {
        private readonly IUserQuizAnswerRepository _userQuizAnswerRepository;
        private readonly IUserRepository _userRepository;

        public UserQuizAnswerService(IUserQuizAnswerRepository userQuizAnswerRepository
            , IUserRepository userRepository)
        {
            _userQuizAnswerRepository = userQuizAnswerRepository;
            _userRepository = userRepository;

        }

        private UserQuizAnswerResponse MapToUserQuizAnswerResponse(UserQuizAnswer userQuizAnswer)
        {
            return new UserQuizAnswerResponse
            {
                UserQuizAnswerId = userQuizAnswer.UserQuizAnswerId,
                UserId = userQuizAnswer.UserId,
                QuestionId = userQuizAnswer.QuestionId,
                SelectedOptionId = userQuizAnswer.SelectedOptionId,
                AnswerText = userQuizAnswer.AnswerText,
                AnsweredAt = userQuizAnswer.AnsweredAt
            };
        }
        public async Task<Result<UserQuizAnswerResponse>> AddUserQuizAnswerAsync(UserQuizAnswerCreateRequest request)
        {
            try
            {
                if(request == null)
                {
                    return Result<UserQuizAnswerResponse>.Invalid("Request cannot be null.");
                }
                var newUserQuizAnswer = new UserQuizAnswer
                {
                    UserId = request.UserId,
                    QuestionId = request.QuestionId,
                    SelectedOptionId = request.SelectedOptionId,
                    AnswerText = request.AnswerText,
                    AnsweredAt = DateTime.Now
                };
                var tmp = await _userQuizAnswerRepository.AddUserQuizAnswerAsync(newUserQuizAnswer);
                return Result<UserQuizAnswerResponse>.Success(MapToUserQuizAnswerResponse(tmp), "Added successfully");
            }
            catch (Exception ex)
            {
                return Result<UserQuizAnswerResponse>.Error($"An error occurred while adding the user quiz answer: {ex.Message}");
            }
        }

        public async Task<Result<UserQuizAnswerResponse>> DeleteUserQuizAnswerAsync(Guid userQuizAnswerId)
        {
            try
            {
                var tmp = await _userQuizAnswerRepository.GetUserQuizAnswerByIdAsync(userQuizAnswerId);
                if (tmp == null)
                {
                    return Result<UserQuizAnswerResponse>.NotFound("User quiz answer not found.");
                }
                var response = await _userQuizAnswerRepository.DeleteUserQuizAnswerAsync(userQuizAnswerId);
                return Result<UserQuizAnswerResponse>.Success(MapToUserQuizAnswerResponse(response), "Deleted successfully");
            }
            catch(Exception ex)
            {
                return Result<UserQuizAnswerResponse>.Error($"An error occurred while deleting the user quiz answer: {ex.Message}");
            }
        }

        public async Task<Result<UserQuizAnswerResponse?>> GetUserQuizAnswerByIdAsync(Guid userQuizAnswerId)
        {
            try
            {
                var userQuizAnswer = await _userQuizAnswerRepository.GetUserQuizAnswerByIdAsync(userQuizAnswerId);
                if (userQuizAnswer == null)
                {
                    return Result<UserQuizAnswerResponse?>.NotFound("User quiz answer not found.");
                }
                return Result<UserQuizAnswerResponse?>.Success(MapToUserQuizAnswerResponse(userQuizAnswer), "Retrieved successfully");
            }
            catch(Exception ex)
            {
                return Result<UserQuizAnswerResponse?>.Error($"An error occurred while retrieving the user quiz answer: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserQuizAnswerResponse>>> GetUserQuizAnswersAsync()
        {
            try
            {
                var userQuizAnswers = await _userQuizAnswerRepository.GetUserQuizAnswersAsync();
                if (userQuizAnswers == null || !userQuizAnswers.Any())
                {
                    return Result<IEnumerable<UserQuizAnswerResponse>>.NotFound("No user quiz answers found.");
                }
                var response = userQuizAnswers.Select(tmp => MapToUserQuizAnswerResponse(tmp));
                return Result<IEnumerable<UserQuizAnswerResponse>>.Success(response, "Retrieved successfully");
            }
            catch(Exception ex)
            {
                return Result<IEnumerable<UserQuizAnswerResponse>>.Error($"An error occurred while retrieving user quiz answers: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserQuizAnswerResponse>>> GetUserQuizAnswersByQuestionIdAsync(Guid questionId)
        {
            try
            {
                var userQuizAnswers = await _userQuizAnswerRepository.GetUserQuizAnswersByQuestionIdAsync(questionId);
                if (userQuizAnswers == null || !userQuizAnswers.Any())
                {
                    return Result<IEnumerable<UserQuizAnswerResponse>>.NotFound("No user quiz answers found for this question.");
                }
                var response = userQuizAnswers.Select(tmp => MapToUserQuizAnswerResponse(tmp));
                return Result<IEnumerable<UserQuizAnswerResponse>>.Success(response, "Retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserQuizAnswerResponse>>.Error($"An error occurred while retrieving user quiz answers by question ID: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserQuizAnswerResponse>>> GetUserQuizAnswersByUserIdAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return Result<IEnumerable<UserQuizAnswerResponse>>.NotFound("User not found.");
                }
                var userQuizAnswers = await _userQuizAnswerRepository.GetUserQuizAnswersByUserIdAsync(userId);
                if (userQuizAnswers == null || !userQuizAnswers.Any())
                {
                    return Result<IEnumerable<UserQuizAnswerResponse>>.NotFound("No user quiz answers found for this user.");
                }
                var response = userQuizAnswers.Select(tmp => MapToUserQuizAnswerResponse(tmp));
                return Result<IEnumerable<UserQuizAnswerResponse>>.Success(response, "Retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserQuizAnswerResponse>>.Error($"An error occurred while retrieving user quiz answers by user ID: {ex.Message}");
            }
        }

        public async Task<Result<UserQuizAnswerResponse>> UpdateUserQuizAnswerAsync(Guid userQuizAnswerId, UserQuizAnswerUpdateRequest request)
        {
            try
            {
                if (request == null)
                {
                    return Result<UserQuizAnswerResponse>.Invalid("Request cannot be null.");
                }
                var existingAnswer = await _userQuizAnswerRepository.GetUserQuizAnswerByIdAsync(userQuizAnswerId);
                if (existingAnswer == null)
                {
                    return Result<UserQuizAnswerResponse>.NotFound("User quiz answer not found.");
                }
                existingAnswer.SelectedOptionId = request.SelectedOptionId;
                existingAnswer.AnswerText = request.AnswerText;
                existingAnswer.AnsweredAt = DateTime.Now;
                var updatedAnswer = await _userQuizAnswerRepository.UpdateUserQuizAnswerAsync(existingAnswer);
                return Result<UserQuizAnswerResponse>.Success(MapToUserQuizAnswerResponse(updatedAnswer), "Updated successfully");
            }
            catch (Exception ex)
            {
                return Result<UserQuizAnswerResponse>.Error($"An error occurred while updating the user quiz answer: {ex.Message}");
            }
        }
    }
}
