using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ICourseWeekService
    {
        Task<Result<CourseWeek>> AddNewCourseWeekAsync(CourseWeek courseWeek);
        Task<Result<IEnumerable<CourseWeek>>> GetAllCourseWeeksAsync();
        Task<Result<CourseWeek>> GetCourseWeekByIdAsync(Guid id);
        Task<Result<bool>> DeleteCourseWeekByIdAsync(Guid id);
        Task<Result<CourseWeek>> UpdateCourseWeekAsync(Guid id, CourseWeek courseWeek);
    }
} 