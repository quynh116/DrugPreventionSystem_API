using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ILessonResourceService
    {
        Task<Result<LessonResource>> AddNewLessonResourceAsync(LessonResourceRequest lessonResource);
        Task<Result<IEnumerable<LessonResource>>> GetAllLessonResourcesAsync();
        Task<Result<LessonResource>> GetLessonResourceByIdAsync(Guid id);
        Task<Result<bool>> DeleteLessonResourceByIdAsync(Guid id);
        Task<Result<LessonResource>> UpdateLessonResourceAsync(Guid id, LessonResource lessonResource);
        Task<Result<IEnumerable<LessonResourceResponse>>> GetResourcesByLessonIdAsync(Guid lessonId);
    }
} 