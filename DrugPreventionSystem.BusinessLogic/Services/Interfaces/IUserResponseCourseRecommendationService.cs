using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserResponseCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserResponseCourseRecommendation;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserResponseCourseRecommendationService
    {
        Task<Result<UserResponseCourseRecommendationResponse>> AddUserResponseAsync(UserResponseCourseRecommendationCreateRequest request);
        Task<Result<UserResponseCourseRecommendationResponse>> DeleteUserResponseAsync(Guid userRecId);
        Task<Result<UserResponseCourseRecommendationResponse>> GetRecommendationsByUserRecIdAsync(Guid userRecId);
        Task<Result<IEnumerable<UserResponseCourseRecommendationResponse>>> GetUsersByResponseIdAsync(Guid responseId);
        Task<Result<IEnumerable<CourseResponse>>> GetRecommendedCoursesByResponseIdAsync(Guid responseId);
        Task<Result<UserResponseCourseRecommendationResponse>> UpdateUserResponseAsync(Guid id, UserResponseCourseRecommendationUpdateRequest request);
    }
}
