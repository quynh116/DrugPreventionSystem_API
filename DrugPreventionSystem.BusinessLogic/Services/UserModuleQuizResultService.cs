using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserModuleQuizResult;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserLessonProgress;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserModuleQuizResult;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repositories;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserModuleQuizResultService : IUserModuleQuizResultService
    {
        private readonly IUserModuleQuizResultRepository _userModuleQuizResultRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;

        public UserModuleQuizResultService(IUserModuleQuizResultRepository userModuleQuizResultRepository, ILessonRepository lessonRepository, IUserRepository userRepository)
        {
            _userModuleQuizResultRepository = userModuleQuizResultRepository;
            _lessonRepository = lessonRepository;
            _userRepository = userRepository;
        }

        private UserModuleQuizResultResponse MapToUserModuleQuizResultResponse(UserModuleQuizResult result)
        {
            return new UserModuleQuizResultResponse
            {
                ResultId = result.ResultId,
                UserId = result.UserId,
                LessonId = result.LessonId,
                TotalScore = result.TotalScore,
                CorrectCount = result.CorrectCount,
                TotalQuestions = result.TotalQuestions,
                Status = result.Status,
                TakenAt = result.TakenAt
            };
        }
        public async Task<Result<UserModuleQuizResultResponse>> AddUserModuleQuizResultAsync(UserModuleQuizResultCreateRequest request)
        {
            try
            {
                var result = new UserModuleQuizResult
                {
                    ResultId = Guid.NewGuid(),
                    UserId = request.UserId,
                    LessonId = request.LessonId,
                    TotalScore = request.TotalScore,
                    CorrectCount = request.CorrectCount,
                    TotalQuestions = request.TotalQuestions,
                    Status = request.Status,
                    TakenAt = request.TakenAt
                };
                await _userModuleQuizResultRepository.AddUserModuleQuizResultAsync(result);
                var response = MapToUserModuleQuizResultResponse(result);
                return Result<UserModuleQuizResultResponse>.Success(response, "Added successfully");
            }
            catch (Exception ex)
            {
                return Result<UserModuleQuizResultResponse>.Error($"Error adding user module quiz result: {ex.Message}");
            }
        }

        public async Task<Result<UserModuleQuizResultResponse>> DeleteUserModuleQuizResultAsync(Guid resultId)
        {
            try
            {
                var umqr = await _userModuleQuizResultRepository.GetUserModuleQuizResultByIdAsync(resultId);
                if(umqr == null)
                {
                    return Result<UserModuleQuizResultResponse>.NotFound("User module quiz result not found");
                }
                await _userModuleQuizResultRepository.DeleteUserModuleQuizResultAsync(resultId);
                return Result<UserModuleQuizResultResponse>.Success(MapToUserModuleQuizResultResponse(umqr), "Deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<UserModuleQuizResultResponse>.Error($"Error deleting user module quiz result: {ex.Message}");
            }
        }

        public async Task<Result<UserModuleQuizResultResponse?>> GetUserModuleQuizResultByIdAsync(Guid resultId)
        {
            try
            {
                var result = await _userModuleQuizResultRepository.GetUserModuleQuizResultByIdAsync(resultId);
                if (result == null)
                {
                    return Result<UserModuleQuizResultResponse?>.NotFound("User module quiz result not found");
                }
                return Result<UserModuleQuizResultResponse?>.Success(MapToUserModuleQuizResultResponse(result), "User Module Quiz Result retrived succesfully");
            }
            catch (Exception ex)
            {
                return Result<UserModuleQuizResultResponse?>.Error($"Error retrieving user module quiz result: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserModuleQuizResultResponse>>> GetUserModuleQuizResultsAsync()
        {
            try
            {
                var result = await _userModuleQuizResultRepository.GetUserModuleQuizResultsAsync();
                if (result == null || !result.Any())
                {
                    return Result<IEnumerable<UserModuleQuizResultResponse>>.NotFound("No user module quiz results found");
                }
                var response = result.Select(umqr => MapToUserModuleQuizResultResponse(umqr)).ToList();
                return Result<IEnumerable<UserModuleQuizResultResponse>>.Success(response, "User Module Quiz Results retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserModuleQuizResultResponse>>.Error($"Error retrieving user module quiz results: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserModuleQuizResultResponse>>> GetUserModuleQuizResultsByLessonIdAsync(Guid lessonId)
        {
            try
            {
                var lesson = await _lessonRepository.GetLessonByIdAsync(lessonId);
                if (lesson == null)
                {
                    return Result<IEnumerable<UserModuleQuizResultResponse>>.NotFound($"Lesson with ID {lessonId} not found.");
                }
                var results = await _userModuleQuizResultRepository.GetUserModuleQuizResultsByLessonIdAsync(lessonId);
                if (results == null || !results.Any())
                {
                    return Result<IEnumerable<UserModuleQuizResultResponse>>.NotFound("No user module quiz results found for the specified lesson");
                }
                var response = results.Select(umqr => MapToUserModuleQuizResultResponse(umqr)).ToList();
                return Result<IEnumerable<UserModuleQuizResultResponse>>.Success(response, "User Module Quiz Results by lesson retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserModuleQuizResultResponse>>.Error($"Error retrieving user module quiz results by lesson: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserModuleQuizResultResponse>>> GetUserModuleQuizResultsByUserIdAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return Result<IEnumerable<UserModuleQuizResultResponse>>.NotFound($"User with ID {userId} not found.");
                }
                var results = await _userModuleQuizResultRepository.GetUserModuleQuizResultsByUserIdAsync(userId);
                if (results == null || !results.Any())
                {
                    return Result<IEnumerable<UserModuleQuizResultResponse>>.NotFound("No user module quiz results found for the specified user");
                }
                var response = results.Select(umqr => MapToUserModuleQuizResultResponse(umqr)).ToList();
                return Result<IEnumerable<UserModuleQuizResultResponse>>.Success(response, "User Module Quiz Results by user retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserModuleQuizResultResponse>>.Error($"Error retrieving user module quiz results by user: {ex.Message}");
            }
        }

        public async Task<Result<UserModuleQuizResultResponse>> UpdateUserModuleQuizResultAsync(Guid resultId, UserModuleQuizResultUpdateRequest request)
        {
            try
            {
                var result = await _userModuleQuizResultRepository.GetUserModuleQuizResultByIdAsync(resultId);
                if (result == null)
                {
                    return Result<UserModuleQuizResultResponse>.NotFound("User module quiz result not found");
                }
                if(request == null)
                {
                    return Result<UserModuleQuizResultResponse>.Invalid("Request cannot be null");
                }
                result.UserId = request.UserId;
                result.LessonId = request.LessonId;
                result.TotalScore = request.TotalScore;
                result.CorrectCount = request.CorrectCount;
                result.TotalQuestions = request.TotalQuestions;
                result.Status = request.Status;
                result.TakenAt = DateTime.Now;
                await _userModuleQuizResultRepository.UpdateUserModuleQuizResultAsync(result);
                var response = MapToUserModuleQuizResultResponse(result);
                return Result<UserModuleQuizResultResponse>.Success(response, "User Module Quiz Result updated successfully");
            }
            catch (Exception ex)
            {
                return Result<UserModuleQuizResultResponse>.Error($"Error updating user module quiz result: {ex.Message}");
            }
        }
    }
}
