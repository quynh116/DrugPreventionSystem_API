using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Course;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repositories;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class CourseCertificateService : ICourseCertificateService
    {
        private readonly ICourseCertificateRepository _repo;
        private readonly IUserCourseEnrollmentRepository _enrollmentRepository;
        private readonly IUserLessonProgressRepository _userLessonProgressRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICertificateService _certificateService;
        private readonly IPhotoService _photoService;

        public CourseCertificateService(ICourseCertificateRepository repo,
            IUserCourseEnrollmentRepository enrollmentRepository,
            IUserLessonProgressRepository userLessonProgressRepository,
            ICourseRepository courseRepository,
            IUserRepository userRepository,
            ICertificateService certificateService,
            IPhotoService photoService)
        {
            _repo = repo;
            _enrollmentRepository = enrollmentRepository;
            _userLessonProgressRepository = userLessonProgressRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _certificateService = certificateService;
            _photoService = photoService;
        }
        private CourseCertificateResponse MapToResponse(CourseCertificate entity)
        {
            return new CourseCertificateResponse
            {
                CertificateId = entity.CertificateId,
                UserId = entity.UserId,
                CourseId = entity.CourseId,
                IssuedAt = entity.IssuedAt,
                CertificateUrl = entity.CertificateUrl
            };
        }
        public async Task<Result<CourseCertificateResponse>> AddAsync(CourseCertificateRequest request)
        {
            var entity = new CourseCertificate
            {
                UserId = request.UserId,
                CourseId = request.CourseId,
                IssuedAt = request.IssuedAt ?? DateTime.Now,
                CertificateUrl = request.CertificateUrl
            };
            var added = await _repo.AddAsync(entity);
            return Result<CourseCertificateResponse>.Success(MapToResponse(added), "Added successfully");
        }
        public async Task<Result<IEnumerable<CourseCertificateResponse>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return Result<IEnumerable<CourseCertificateResponse>>.Success(list.Select(MapToResponse));
        }
        public async Task<Result<CourseCertificateResponse>> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<CourseCertificateResponse>.NotFound($"Not found CourseCertificate with id: {id}");
            return Result<CourseCertificateResponse>.Success(MapToResponse(entity));
        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }
        public async Task<Result<CourseCertificateResponse>> UpdateAsync(Guid id, CourseCertificateRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return Result<CourseCertificateResponse>.NotFound($"Not found CourseCertificate with id: {id}");
            entity.UserId = request.UserId;
            entity.CourseId = request.CourseId;
            entity.IssuedAt = request.IssuedAt ?? entity.IssuedAt;
            entity.CertificateUrl = request.CertificateUrl;
            await _repo.UpdateAsync(entity);
            return Result<CourseCertificateResponse>.Success(MapToResponse(entity), "Updated successfully");
        }
        public async Task<Result<IEnumerable<CourseCertificateResponse>>> GetByUserIdAsync(Guid userId)
        {
            var list = await _repo.GetByUserIdAsync(userId);
            return Result<IEnumerable<CourseCertificateResponse>>.Success(list.Select(MapToResponse));
        }
        public async Task<Result<IEnumerable<CourseCertificateResponse>>> GetByCourseIdAsync(Guid courseId)
        {
            var list = await _repo.GetByCourseIdAsync(courseId);
            return Result<IEnumerable<CourseCertificateResponse>>.Success(list.Select(MapToResponse));
        }

        public async Task<Result<CertificateDetailsResponse>> GetOrCreateCertificateUrlAsync(Guid userId, Guid courseId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Result<CertificateDetailsResponse>.NotFound("User not found.");
            }

            var course = await _courseRepository.GetByIdAsync(courseId); 
            if (course == null)
            {
                return Result<CertificateDetailsResponse>.NotFound("Course not found.");
            }

            var enrollment = await _enrollmentRepository.GetByUserIdAndCourseIdAsync(userId, courseId);
            if (enrollment == null)
            {
                return Result<CertificateDetailsResponse>.NotFound("User is not enrolled in this course.");
            }

            // 2. Kiểm tra tiến độ hoàn thành khóa học
            int totalLessonsInCourse = course.CourseWeeks?.SelectMany(cw => cw.Lessons).Count() ?? 0;
            if (totalLessonsInCourse == 0)
            {
                return Result<CertificateDetailsResponse>.Error("Course has no lessons, certificate cannot be issued.");
            }

            var completedLessonsByUser = await _userLessonProgressRepository.CountCompletedLessonsForUserInCourseAsync(userId, courseId);
            float courseProgressPercentage = (float)completedLessonsByUser / totalLessonsInCourse * 100;

            if ((int)Math.Round(courseProgressPercentage) < 100)
            {
                return Result<CertificateDetailsResponse>.Error("Course not yet completed. Progress: " + (int)Math.Round(courseProgressPercentage) + "%");
            }

            // 3. Lấy hoặc tạo chứng chỉ
            var existingCertificate = await _repo.GetByUserIdAndCourseIdAsync(userId, courseId);

            CourseCertificate certificateToReturn;
            if (existingCertificate != null)
            {
                certificateToReturn = existingCertificate;
            }
            else
            {
                var certificateData = new CertificateData
                {
                    UserName = user.Username,
                    CourseTitle = course.Title,
                    CompletionDate = DateTime.Now, // Sẽ được cập nhật sau
                    DurationWeeks = course.TotalDuration.HasValue ? $"{course.TotalDuration} tuần" : "N/A",
                    InstructorName = course.Instructor?.FullName ?? "N/A",
                };

                string certificateUrl = await _certificateService.GenerateCertificateWithTemplateAsync(certificateData);

                var newCertificate = new CourseCertificate
                {
                    UserId = userId,
                    CourseId = courseId,
                    IssuedAt = DateTime.Now, // Ngày cấp chứng chỉ là ngày tạo mới
                    CertificateUrl = certificateUrl
                };
                certificateToReturn = await _repo.AddAsync(newCertificate);
            }

            
            DateTime completionDate = certificateToReturn.IssuedAt;
            var latestCompletedLessonProgress = (await _userLessonProgressRepository.GetAllUserLessonProgressesByUserIdAsync(userId))
                                                    .Where(ulp => ulp.Lesson?.CourseWeek?.CourseId == courseId && ulp.Passed)
                                                    .OrderByDescending(ulp => ulp.CompletedAt)
                                                    .FirstOrDefault();
            if (latestCompletedLessonProgress != null && latestCompletedLessonProgress.CompletedAt.HasValue)
            {
                completionDate = latestCompletedLessonProgress.CompletedAt.Value;
            }
            else if (enrollment.CompletedAt.HasValue) 
            {
                completionDate = enrollment.CompletedAt.Value;
            }


            // 5. Build và trả về CertificateDetailsResponse
            var response = new CertificateDetailsResponse
            {
                CertificateId = certificateToReturn.CertificateId.ToString(),
                CertificateUrl = certificateToReturn.CertificateUrl ?? string.Empty,
                UserName =user.Username, // Giả sử User có FullName
                CourseTitle = course.Title,
                CompletionDate = completionDate,
                DurationWeeks = course.TotalDuration.HasValue ? $"{course.TotalDuration} tuần" : "N/A", // Format lại
                InstructorName = course.Instructor?.FullName ?? "N/A", // Giả sử Course có Instructor và Instructor có FullName
                IssuingOrganization = "Trung tâm Phòng chống Ma túy Quốc gia" // Hoặc lấy từ cấu hình
            };

            return Result<CertificateDetailsResponse>.Success(response);
        }
    }
} 