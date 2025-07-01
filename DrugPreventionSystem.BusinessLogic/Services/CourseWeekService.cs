using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Course;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
using DrugPreventionSystem.DataAccess.Repository;
using Azure.Core;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class CourseWeekService : ICourseWeekService
    {
        private readonly ICourseWeekRepository _courseWeekRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseWeekService(ICourseWeekRepository courseWeekRepository, ICourseRepository courseRepository)
        {
            _courseWeekRepository = courseWeekRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Result<CourseWeekResponse>> AddNewCourseWeekAsync(CourseWeekRequest courseWeek)
        {
            var course = await _courseRepository.GetByIdAsync(courseWeek.CourseId);
            if(course == null)
            {
                return Result<CourseWeekResponse>.NotFound($"Course with ID {courseWeek.CourseId} not found.");
            }
            var newCourseWeek = new CourseWeek()
            {
                CourseId = courseWeek.CourseId,
                Title = courseWeek.Title,
                WeekNumber = courseWeek.WeekNumber,
            };
            var added = await _courseWeekRepository.AddNewCourseWeek(newCourseWeek);
            course.TotalDuration = (course.TotalDuration ?? 0) + 1;
            await _courseRepository.UpdateAsync(course);
            return Result<CourseWeekResponse>.Success(MapCourseWeekToResponse(added), "Added successfully");
        }

        public async Task<Result<IEnumerable<CourseWeekResponse>>> GetAllCourseWeeksAsync()
        {
            var list = await _courseWeekRepository.GetAllCourseWeeksAsync();
            var responseList = new List<CourseWeekResponse>();
            foreach (var cw in list)
            {
                responseList.Add(MapCourseWeekToResponse(cw));
            }
            return Result<IEnumerable<CourseWeekResponse>>.Success(responseList);
        }

        public async Task<Result<CourseWeekResponse>> GetCourseWeekByIdAsync(Guid id)
        {
            var cw = await _courseWeekRepository.GetCourseWeekByIdAsync(id);
            if (cw == null) return Result<CourseWeekResponse>.NotFound($"Not found CourseWeek with id: {id}");
            return Result<CourseWeekResponse>.Success(MapCourseWeekToResponse(cw));
        }

        public async Task<Result<bool>> DeleteCourseWeekByIdAsync(Guid id)
        {
            await _courseWeekRepository.DeleteCourseWeekByIdAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }

        public async Task<Result<CourseWeekResponse>> UpdateCourseWeekAsync(Guid id, CourseWeekRequest courseWeek)
        {
            var existing = await _courseWeekRepository.GetCourseWeekByIdAsync(id);
            if (existing == null) return Result<CourseWeekResponse>.NotFound($"Not found CourseWeek with id: {id}");
            // Update fields
            existing.Title = courseWeek.Title;
            existing.WeekNumber = courseWeek.WeekNumber;
            await _courseWeekRepository.UpdateCourseWeekAsync(existing);
            return Result<CourseWeekResponse>.Success(MapCourseWeekToResponse(existing), "Updated successfully");
        }

        private CourseWeekResponse MapCourseWeekToResponse(CourseWeek cw)
        {
            return new CourseWeekResponse
            {
                WeekId = cw.WeekId,
                CourseId = cw.CourseId,
                Title = cw.Title,
                WeekNumber = cw.WeekNumber,
                Lessons = cw.Lessons?.Select(l => new LessonResponse
                {
                    LessonId = l.LessonId,
                    Title = l.Title,
                    DurationMinutes = l.DurationMinutes,
                    Sequence = l.Sequence,
                    HasQuiz = l.HasQuiz,
                    HasPractice = l.HasPractice,
                    CreatedAt = l.CreatedAt,
                    Resources = l.LessonResources?.Select(r => new LessonResourceResponse
                    {
                        ResourceId = r.ResourceId,
                        LessonId = r.LessonId,
                        ResourceType = r.ResourceType,
                        ResourceUrl = r.ResourceUrl,
                        Description = r.Description
                    }).ToList() ?? new List<LessonResourceResponse>()
                }).ToList() ?? new List<LessonResponse>()
            };
        }
    }
} 