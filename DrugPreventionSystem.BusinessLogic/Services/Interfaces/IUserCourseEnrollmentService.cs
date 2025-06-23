using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserCourseEnrollment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserCourseEnrollmentService
    {
        Task<Result<UserCourseEnrollmentResponse>> AddAsync(UserCourseEnrollmentRequest request);
        Task<Result<IEnumerable<UserCourseEnrollmentResponse>>> GetAllAsync();
        Task<Result<UserCourseEnrollmentResponse>> GetByIdAsync(Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<UserCourseEnrollmentResponse>> UpdateAsync(Guid id, UserCourseEnrollmentUpdateRequest request);
        Task<Result<IEnumerable<UserCourseEnrollmentResponse>>> GetByUserIdAsync(Guid userId);
        Task<Result<IEnumerable<UserCourseEnrollmentResponse>>> GetByCourseIdAsync(Guid courseId);
        Task<Result<UserCourseEnrollmentStatusResponse>> CheckUserCourseEnrollmentStatusAsync(Guid courseId, Guid userId);
    }
} 