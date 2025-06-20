using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ILessonService
    {
        Task<Result<Lesson>> AddNewLessonAsync(LessonRequest lesson);
        Task<Result<IEnumerable<Lesson>>> GetAllLessonsAsync();
        Task<Result<Lesson>> GetLessonByIdAsync(Guid id);
        Task<Result<bool>> DeleteLessonByIdAsync(Guid id);
        Task<Result<Lesson>> UpdateLessonAsync(Guid id, Lesson lesson);
    }
} 