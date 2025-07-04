using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Course;
using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ICourseWeekService
    {
        Task<Result<CourseWeekResponse>> AddNewCourseWeekAsync(CourseWeekRequest courseWeek);
        Task<Result<IEnumerable<CourseWeekResponse>>> GetAllCourseWeeksAsync();
        Task<Result<CourseWeekResponse>> GetCourseWeekByIdAsync(Guid id);
        Task<Result<bool>> DeleteCourseWeekByIdAsync(Guid id);
        Task<Result<CourseWeekResponse>> UpdateCourseWeekAsync(Guid id, CourseWeekRequest courseWeek);
        
    }
} 