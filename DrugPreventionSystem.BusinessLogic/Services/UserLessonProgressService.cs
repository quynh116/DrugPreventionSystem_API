using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserLessonProgress;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserLessonProgress;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repositories;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserLessonProgressService : IUserLessonProgressService
    {
        private readonly IUserLessonProgressRepository _userLessonProgressRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;

        public UserLessonProgressService(IUserLessonProgressRepository userLessonProgressRepository, ILessonRepository lessonRepository, IUserRepository userRepository)
        {
            _userLessonProgressRepository = userLessonProgressRepository;
            _lessonRepository = lessonRepository;
            _userRepository = userRepository;
        }
        private UserLessonProgressResponse MapToUserLessonProgressResponse(UserLessonProgress userLessonProgress)
        {
            if(userLessonProgress == null)
            {
                return null;
            }
            return new UserLessonProgressResponse
            {
                ProgressId = userLessonProgress.ProgressId,
                UserId = userLessonProgress.UserId,
                LessonId = userLessonProgress.LessonId,
                CompletedAt = userLessonProgress.CompletedAt,
                QuizScore = userLessonProgress.QuizScore,
                Passed = userLessonProgress.Passed,
                UpdatedAt = userLessonProgress.UpdatedAt
            };
        }
        public async Task<Result<UserLessonProgressResponse>> AddUserLessonProgressAsync(UserLessonProgressCreateRequest request)
        {
            try
            {
                var result = new UserLessonProgress
                {
                    ProgressId = Guid.NewGuid(),
                    UserId = request.UserId,
                    LessonId = request.LessonId,
                    CompletedAt = request.CompletedAt,
                    QuizScore = request.QuizScore,
                    Passed = request.Passed,
                    UpdatedAt = DateTime.Now
                };
                await _userLessonProgressRepository.AddUserLessonProgressAsync(result);
                return Result<UserLessonProgressResponse>.Success(MapToUserLessonProgressResponse(result), "User lesson progress added successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserLessonProgressResponse>.Error($"An error occurred while adding user lesson progress: {ex.Message}");
            }
        }

        public async Task<Result<UserLessonProgressResponse>> DeleteUserLessonProgressAsync(Guid progressId)
        {
            try
            {
                var userLessonProgress = await _userLessonProgressRepository.DeleteUserLessonProgressAsync(progressId);
                if (userLessonProgress == null)
                {
                    return Result<UserLessonProgressResponse>.NotFound("User lesson progress not found.");
                }
                return Result<UserLessonProgressResponse>.Success(MapToUserLessonProgressResponse(userLessonProgress), "User lesson progress deleted successfully.");
            }
            catch(Exception ex)
            {
                return Result<UserLessonProgressResponse>.Error($"An error occurred while deleting user lesson progress: {ex.Message}");
            }
        }

        public async Task<Result<UserLessonProgressResponse?>> GetUserLessonProgressByIdAsync(Guid progressId)
        {
            try
            {
                if(progressId == Guid.Empty)
                {
                    return Result<UserLessonProgressResponse?>.Error("Progress ID cannot be empty.");
                }
                var userLessonProgress = await _userLessonProgressRepository.GetUserLessonProgressByIdAsync(progressId);
                if(userLessonProgress == null)
                {
                    return Result<UserLessonProgressResponse?>.NotFound($"User lesson progress with ID {progressId} not found.");
                }
                return Result<UserLessonProgressResponse?>.Success(MapToUserLessonProgressResponse(userLessonProgress), "User lesson progress retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserLessonProgressResponse?>.Error($"An error occurred while retrieving user lesson progress: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserLessonProgressResponse>>> GetUserLessonProgressByLessonIdAsync(Guid lessonId)
        {
            try
            {
                var lesson = await _lessonRepository.GetLessonByIdAsync(lessonId);
                if(lesson == null)
                {
                    return Result<IEnumerable<UserLessonProgressResponse>>.NotFound($"Lesson with ID {lessonId} not found.");
                }
                var list = await _userLessonProgressRepository.GetUserLessonProgressByLessonIdAsync(lessonId);
                if(list == null || !list.Any())
                {
                    return Result<IEnumerable<UserLessonProgressResponse>>.NotFound($"No user lesson progress found for lesson ID {lessonId}.");
                }
                var response = list.Select(ulp => MapToUserLessonProgressResponse(ulp)).ToList();
                return Result<IEnumerable<UserLessonProgressResponse>>.Success(response, "User lesson progress by lesson ID retrieved successfully.");
            }
            catch(Exception ex)
            {
                return Result<IEnumerable<UserLessonProgressResponse>>.Error($"An error occurred while retrieving user lesson progress by lesson ID: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserLessonProgressResponse>>> GetUserLessonProgressByUserIdAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return Result<IEnumerable<UserLessonProgressResponse>>.NotFound($"User with ID {userId} not found.");
                }
                var list = await _userLessonProgressRepository.GetUserLessonProgressByUserIdAsync(userId);
                if(list == null || !list.Any())
                {
                    return Result<IEnumerable<UserLessonProgressResponse>>.NotFound($"No user lesson progress found for user ID {userId}.");
                }
                var response = list.Select(ulp => MapToUserLessonProgressResponse(ulp)).ToList();
                return Result<IEnumerable<UserLessonProgressResponse>>.Success(response, "User lesson progress by user ID retrieved successfully.");
            }
            catch(Exception ex)
            {
                return Result<IEnumerable<UserLessonProgressResponse>>.Error($"An error occurred while retrieving user lesson progress by user ID: {ex.Message}");
            }

        }

        public async Task<Result<IEnumerable<UserLessonProgressResponse>>> GetUserLessonProgressesAsync()
        {
            try
            {
                var list = await _userLessonProgressRepository.GetUserLessonProgressesAsync();
                if (list == null || !list.Any())
                {
                    return Result<IEnumerable<UserLessonProgressResponse>>.NotFound("No user lesson progress found.");
                }
                var response = list.Select(ulp => MapToUserLessonProgressResponse(ulp)).ToList();
                return Result<IEnumerable<UserLessonProgressResponse>>.Success(response, "User lesson progresses retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserLessonProgressResponse>>.Error($"An error occurred while retrieving user lesson progresses: {ex.Message}");
            }
        }

        public async Task<Result<UserLessonProgressResponse>> UpdateUserLessonProgressAsync(Guid progressId, UserLessonProgressUpdateRequest request)
        {
            try
            {
                var ulp = await _userLessonProgressRepository.GetUserLessonProgressByIdAsync(progressId);
                if (ulp == null)
                {
                    return Result<UserLessonProgressResponse>.NotFound($"User lesson progress with ID {progressId} not found.");
                }
                if(request == null)
                {
                    return Result<UserLessonProgressResponse>.Invalid("Update information invalid");
                }
                ulp.LessonId = request.LessonId;
                ulp.UserId = request.UserId;
                ulp.CompletedAt = request.CompletedAt;
                ulp.QuizScore = request.QuizScore;
                ulp.Passed = request.Passed;
                ulp.UpdatedAt = DateTime.Now;
                var updatedUlp = await _userLessonProgressRepository.UpdateUserLessonProgressAsync(ulp);
                return Result<UserLessonProgressResponse>.Success(MapToUserLessonProgressResponse(updatedUlp), "User lesson progress updated successfully.");
            }
            catch(Exception ex)
            {
                return Result<UserLessonProgressResponse>.Error($"An error occurred while updating user lesson progress: {ex.Message}");
            }
        }
    }
}
