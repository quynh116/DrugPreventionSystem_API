using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserResponseCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserResponseCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserResponseCourseRecommendationService : IUserResponseCourseRecommendationService
    {
        private readonly IUserResponseCourseRecommendationRepository _userResponseCourseRecommendationRepository;

        public UserResponseCourseRecommendationService(IUserResponseCourseRecommendationRepository userResponseCourseRecommendationRepository)
        {
            _userResponseCourseRecommendationRepository = userResponseCourseRecommendationRepository;
        }

        private CourseResponse MapToCourseResponse(Course course)
        {
            if (course == null) return null;
            return new CourseResponse
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Description = course.Description,
                AgeGroup = course.AgeGroup,
                IsActive = course.IsActive,
                TotalDuration = course.TotalDuration,
                LessonCount = course.LessonCount,
                StudentCount = course.StudentCount,
                InstructorId = course.InstructorId,
                Requirements = course.Requirements,
                CertificateAvailable = course.CertificateAvailable,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt
            };
        }
        private UserResponseCourseRecommendationResponse MapToUserResponseCourseRecommendationResponse(UserResponseCourseRecommendation userResponse)
        {
            if (userResponse == null) return null;
            return new UserResponseCourseRecommendationResponse
            {
                UserRecId = userResponse.UserRecId,
                ResponseId = userResponse.ResponseId,
                CourseId = userResponse.CourseId,
                RecommendedAt = userResponse.RecommendedAt,
                CreatedAt = userResponse.CreatedAt
            };
        }
        public async Task<Result<UserResponseCourseRecommendationResponse>> AddUserResponseAsync(UserResponseCourseRecommendationCreateRequest request)
        {
            try
            {
                if(request.CourseId == null)
                {
                    return Result<UserResponseCourseRecommendationResponse>.Invalid("Course ID is required.");
                }
                if(request.ResponseId == null)
                {
                    return Result<UserResponseCourseRecommendationResponse>.Invalid("Response ID is required.");
                }
                var userResponse = new UserResponseCourseRecommendation
                {
                    UserRecId = Guid.NewGuid(),
                    ResponseId = request.ResponseId,
                    CourseId = request.CourseId,
                    RecommendedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                };
                await _userResponseCourseRecommendationRepository.AddUserResponseAsync(userResponse);
                return Result<UserResponseCourseRecommendationResponse>
                    .Success(MapToUserResponseCourseRecommendationResponse(userResponse), "User response added successfully.");

            }
            catch (Exception ex)
            {
                return Result<UserResponseCourseRecommendationResponse>.Error($"An error occurred while adding the user response: {ex.Message}");
            }
        }

        public async Task<Result<UserResponseCourseRecommendationResponse>> DeleteUserResponseAsync(Guid userRecId)
        {
            try
            {
                var userResponse = await _userResponseCourseRecommendationRepository.DeleteUserResponseAsync(userRecId);
                if (userResponse == null)
                {
                    return Result<UserResponseCourseRecommendationResponse>.Invalid("User response not found.");
                }
                return Result<UserResponseCourseRecommendationResponse>
                    .Success(MapToUserResponseCourseRecommendationResponse(userResponse), "User response deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseCourseRecommendationResponse>.Error($"An error occurred while deleting the user response: {ex.Message}");
            }
        }

        public async Task<Result<UserResponseCourseRecommendationResponse>> GetRecommendationsByUserRecIdAsync(Guid userRecId)
        {
            try
            {
                var userResponse = await _userResponseCourseRecommendationRepository.GetRecommendationsByUserRecIdAsync(userRecId);
                if (userResponse == null)
                {
                    return Result<UserResponseCourseRecommendationResponse>.Invalid("User response not found.");
                }
                return Result<UserResponseCourseRecommendationResponse>
                    .Success(MapToUserResponseCourseRecommendationResponse(userResponse), "User response retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseCourseRecommendationResponse>.Error($"An error occurred while retrieving the user response: {ex.Message}");
            }
        }
        public async Task<Result<IEnumerable<UserResponseCourseRecommendationResponse>>> GetUsersByResponseIdAsync(Guid responseId)
        {
            try
            {
                var userResponses = await _userResponseCourseRecommendationRepository.GetUsersResponseByResponseIdAsync(responseId);
                if (userResponses == null || !userResponses.Any())
                {
                    return Result<IEnumerable<UserResponseCourseRecommendationResponse>>.Invalid("No user responses found for the given response ID.");
                }
                var responseList = userResponses.Select(MapToUserResponseCourseRecommendationResponse);
                return Result<IEnumerable<UserResponseCourseRecommendationResponse>>
                    .Success(responseList, "User responses retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserResponseCourseRecommendationResponse>>.Error($"An error occurred while retrieving user responses: {ex.Message}");
            }
        }

        public async Task<Result<UserResponseCourseRecommendationResponse>> UpdateUserResponseAsync(Guid id, UserResponseCourseRecommendationUpdateRequest request)
        {
            try
            {
                var userResponse = await _userResponseCourseRecommendationRepository.GetRecommendationsByUserRecIdAsync(id);
                if(userResponse == null)
                {
                    return Result<UserResponseCourseRecommendationResponse>.Invalid("User response not found.");
                }
                if (request.CourseId == null)
                {
                    return Result<UserResponseCourseRecommendationResponse>.Invalid("Course ID is required.");
                }
                if (request.ResponseId == null)
                {
                    return Result<UserResponseCourseRecommendationResponse>.Invalid("Response ID is required.");
                }
                userResponse.ResponseId = request.ResponseId;
                userResponse.CourseId = request.CourseId;
                userResponse.RecommendedAt = DateTime.Now;
                var updatedResponse = await _userResponseCourseRecommendationRepository.UpdateUserResponseAsync(userResponse);
                return Result<UserResponseCourseRecommendationResponse>
                    .Success(MapToUserResponseCourseRecommendationResponse(updatedResponse), "User response updated successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseCourseRecommendationResponse>.Error($"An error occurred while updating the user response: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CourseResponse>>> GetRecommendedCoursesByResponseIdAsync(Guid responseId)
        {
            try
            {
                
                var userRecommendations = await _userResponseCourseRecommendationRepository
                                                .GetUsersResponseByResponseIdWithCoursesAsync(responseId);

                if (userRecommendations == null || !userRecommendations.Any())
                {
                    return Result<IEnumerable<CourseResponse>>.NotFound("Không tìm thấy khóa học được đề xuất cho phản hồi này.");
                }

                
                var courseResponses = userRecommendations
                                        .Select(ur => MapToCourseResponse(ur.Course))
                                        .Where(c => c != null) // Đảm bảo Course không null sau khi mapping
                                        .ToList();

                return Result<IEnumerable<CourseResponse>>.Success(courseResponses, "Danh sách khóa học đề xuất đã được truy xuất thành công.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CourseResponse>>.Error($"Lỗi khi lấy danh sách khóa học đề xuất: {ex.Message}");
            }
        }
    }
}
