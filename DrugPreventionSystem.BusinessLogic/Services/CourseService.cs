using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class CourseService :ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ProvideToken _provideToken;

        public CourseService(ICourseRepository courseRepository, ProvideToken provideToken)
        {
            _courseRepository = courseRepository;
            _provideToken = provideToken;
        }
        private CourseResponse MapToResponse(Course course)
        {
            if (course == null)
            {
                return null;
            }
            return new CourseResponse()
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
                UpdatedAt = course.UpdatedAt,
            };
        }

        public async Task<Result<CourseResponse>> CreateAsync(CourseCreateRequest request)
        {
            var newCourse = new Course()
            {
                CourseId = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                AgeGroup = request.AgeGroup,
                IsActive = request.IsActive,
                TotalDuration = request.TotalDuration,
                LessonCount = request.LessonCount,
                StudentCount = request.StudentCount,
                InstructorId = request.InstructorId,
                Requirements = request.Requirements,
                CertificateAvailable = request.CertificateAvailable,
                CreatedAt = request.CreatedAt,
            };
            var createdCourse = await _courseRepository.CreateAsync(newCourse);
            return Result<CourseResponse>.Success(MapToResponse(createdCourse));
        }

        public async Task<Result<IEnumerable<CourseResponse>>> GetAllAsync()
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync();
                var courseResponses = courses.Select(c => MapToResponse(c)).ToList();
                return Result<IEnumerable<CourseResponse>>.Success(courseResponses);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CourseResponse>>.Error($"Course is null here :{ex.Message}");

            }
        }
        public async Task<Result<CourseResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null)
                {
                    return Result<CourseResponse>.NotFound($"Course with ID {id} not found.");

                }
                return Result<CourseResponse>.Success(MapToResponse(course));
            }
            catch (Exception ex)
            {
                return Result<CourseResponse>.Error($"Course is null here :{ex.Message}");
            }
        }
        public async Task<Result<CourseResponse>> UpdateAsync(CourseUpdateRequest request, Guid id)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null)
                {
                    return Result<CourseResponse>.NotFound($"Course with ID {id} not found.");
                }
                course.Title = request.Title;
                course.Description = request.Description;
                course.AgeGroup = request.AgeGroup;
                course.IsActive = request.IsActive;
                course.TotalDuration = request.TotalDuration;
                course.LessonCount = request.LessonCount;
                course.StudentCount = request.StudentCount;
                course.InstructorId = request.InstructorId;
                course.Requirements = request.Requirements;
                course.CertificateAvailable = request.CertificateAvailable;
                course.UpdatedAt = DateTime.Now;
                await _courseRepository.UpdateAsync(course);
                return Result<CourseResponse>.Success(MapToResponse(course));
            }
            catch (Exception ex)
            {
                return Result<CourseResponse>.Error($"Error updating course: {ex.Message} ");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            try
            {
                if (course == null)
                {
                    return Result<bool>.NotFound($"Course with ID {id} not found.");

                }
                await _courseRepository.DeleteAsync(id);
                return Result<bool>.Success(true, $"Course delete successfully.");

            } catch (Exception ex)
            {
                return Result<bool>.Error($"Course is null here :{ex.Message}");
            }
        }

    }
}
    

