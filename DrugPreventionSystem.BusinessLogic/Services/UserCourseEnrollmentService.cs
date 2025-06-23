using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses.UserCourseEnrollment;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
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
        private readonly ICourseRepository _courseRepository;

        public UserCourseEnrollmentService(IUserCourseEnrollmentRepository repo, ICourseRepository courseRepository)
        {
            _repo = repo;
            _courseRepository = courseRepository;
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
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                return Result<UserCourseEnrollmentResponse>.NotFound($"Course with ID {request.CourseId} not found.");
            }

            var existingEnrollments = await _repo.GetByUserIdAsync(request.UserId);
            if (existingEnrollments.Any(e => e.CourseId == request.CourseId))
            {
                return Result<UserCourseEnrollmentResponse>.Duplicated("User is already enrolled in this course.");
            }
            var entity = new UserCourseEnrollment
            {
                UserId = request.UserId,
                CourseId = request.CourseId
            };
            var added = await _repo.AddAsync(entity);
            course.StudentCount = (course.StudentCount ?? 0) + 1;
            await _courseRepository.UpdateAsync(course);
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
        public async Task<Result<UserCourseEnrollmentResponse>> UpdateAsync(Guid id, UserCourseEnrollmentUpdateRequest request)
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

        public async Task<Result<UserCourseEnrollmentStatusResponse>> CheckUserCourseEnrollmentStatusAsync(Guid courseId, Guid userId)
        {
            try
            {
                var userEnrollment = await _repo.GetByUserIdAndCourseIdAsync(userId, courseId);

                var response = new UserCourseEnrollmentStatusResponse
                {
                    CourseId = courseId,
                    UserId = userId,
                    IsEnrolled = (userEnrollment != null),
                    EnrollmentDate = userEnrollment?.EnrolledAt
                };

                return Result<UserCourseEnrollmentStatusResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<UserCourseEnrollmentStatusResponse>.Error($"Error checking enrollment status: {ex.Message}");
            }
        }
    }
} 