using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserCourseEnrollmentService : IUserCourseEnrollmentService
    {
        private readonly IUserCourseEnrollmentRepository _repo;
        public UserCourseEnrollmentService(IUserCourseEnrollmentRepository repo)
        {
            _repo = repo;
        }
        private UserCourseEnrollmentResponse MapToResponse(UserCourseEnrollment entity)
        {
            return new UserCourseEnrollmentResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                CourseId = entity.CourseId,
                EnrolledAt = entity.EnrolledAt,
                Status = entity.Status,
                CompletedAt = entity.CompletedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
        public async Task<Result<UserCourseEnrollmentResponse>> AddAsync(UserCourseEnrollmentRequest request)
        {
            var entity = new UserCourseEnrollment
            {
                UserId = request.UserId,
                CourseId = request.CourseId,
                Status = request.Status ?? "NotStarted",
                CompletedAt = request.CompletedAt,
                UpdatedAt = request.UpdatedAt
            };
            var added = await _repo.AddAsync(entity);
            return Result<UserCourseEnrollmentResponse>.Success(MapToResponse(added), "Added successfully");
        }
        public async Task<Result<IEnumerable<UserCourseEnrollmentResponse>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return Result<IEnumerable<UserCourseEnrollmentResponse>>.Success(list.Select(MapToResponse));
        }
        public async Task<Result<UserCourseEnrollmentResponse>> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<UserCourseEnrollmentResponse>.NotFound($"Not found UserCourseEnrollment with id: {id}");
            return Result<UserCourseEnrollmentResponse>.Success(MapToResponse(entity));
        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }
        public async Task<Result<UserCourseEnrollmentResponse>> UpdateAsync(Guid id, UserCourseEnrollmentRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<UserCourseEnrollmentResponse>.NotFound($"Not found UserCourseEnrollment with id: {id}");
            entity.Status = request.Status ?? entity.Status;
            entity.CompletedAt = request.CompletedAt;
            entity.UpdatedAt = request.UpdatedAt ?? DateTime.Now;
            await _repo.UpdateAsync(entity);
            return Result<UserCourseEnrollmentResponse>.Success(MapToResponse(entity), "Updated successfully");
        }
        public async Task<Result<IEnumerable<UserCourseEnrollmentResponse>>> GetByUserIdAsync(Guid userId)
        {
            var list = await _repo.GetByUserIdAsync(userId);
            return Result<IEnumerable<UserCourseEnrollmentResponse>>.Success(list.Select(MapToResponse));
        }
        public async Task<Result<IEnumerable<UserCourseEnrollmentResponse>>> GetByCourseIdAsync(Guid courseId)
        {
            var list = await _repo.GetByCourseIdAsync(courseId);
            return Result<IEnumerable<UserCourseEnrollmentResponse>>.Success(list.Select(MapToResponse));
        }
    }
} 