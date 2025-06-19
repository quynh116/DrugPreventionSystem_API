using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class CourseWeekService : ICourseWeekService
    {
        private readonly ICourseWeekRepository _courseWeekRepository;

        public CourseWeekService(ICourseWeekRepository courseWeekRepository)
        {
            _courseWeekRepository = courseWeekRepository;
        }

        public async Task<Result<CourseWeek>> AddNewCourseWeekAsync(CourseWeek courseWeek)
        {
            var added = await _courseWeekRepository.AddNewCourseWeek(courseWeek);
            return Result<CourseWeek>.Success(added, "Added successfully");
        }

        public async Task<Result<IEnumerable<CourseWeek>>> GetAllCourseWeeksAsync()
        {
            var list = await _courseWeekRepository.GetAllCourseWeeksAsync();
            return Result<IEnumerable<CourseWeek>>.Success(list);
        }

        public async Task<Result<CourseWeek>> GetCourseWeekByIdAsync(Guid id)
        {
            var cw = await _courseWeekRepository.GetCourseWeekByIdAsync(id);
            if (cw == null) return Result<CourseWeek>.NotFound($"Not found CourseWeek with id: {id}");
            return Result<CourseWeek>.Success(cw);
        }

        public async Task<Result<bool>> DeleteCourseWeekByIdAsync(Guid id)
        {
            await _courseWeekRepository.DeleteCourseWeekByIdAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }

        public async Task<Result<CourseWeek>> UpdateCourseWeekAsync(Guid id, CourseWeek courseWeek)
        {
            var existing = await _courseWeekRepository.GetCourseWeekByIdAsync(id);
            if (existing == null) return Result<CourseWeek>.NotFound($"Not found CourseWeek with id: {id}");
            // Update fields
            existing.Title = courseWeek.Title;
            existing.WeekNumber = courseWeek.WeekNumber;
            existing.CourseId = courseWeek.CourseId;
            await _courseWeekRepository.UpdateCourseWeekAsync(existing);
            return Result<CourseWeek>.Success(existing, "Updated successfully");
        }
    }
} 