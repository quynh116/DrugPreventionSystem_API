using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Course;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Course;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Result<CourseResponse>> CreateAsync(CourseCreateRequest request);
        Task<Result<IEnumerable<CourseResponse>>> GetAllAsync();
        Task<Result<CourseResponse>> GetByIdAsync(Guid id);
        Task<Result<CourseResponse>> UpdateAsync(CourseUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<IEnumerable<CourseResponse>>> GetCoursesByAgeGroupAsync(string ageGroup);
        Task<Result<CourseContentResponse>> GetCourseContentAsync(Guid courseId);
    }
}
