using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ILessonService
    {
        Task<Result<Lesson>> AddNewLessonAsync(LessonRequest lesson);
        Task<Result<IEnumerable<LessonResponse>>> GetAllLessonsAsync();
        Task<Result<LessonResponse>> GetLessonByIdAsync(Guid id);
        Task<Result<bool>> DeleteLessonByIdAsync(Guid id);
        Task<Result<Lesson>> UpdateLessonAsync(Guid id, Lesson lesson);
    }
} 