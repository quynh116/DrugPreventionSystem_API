using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Result<Lesson>> AddNewLessonAsync(LessonRequest lesson)
        {
            var newLesson = new Lesson()
            {
                WeekId = lesson.WeekId,
                Title = lesson.Title,
                DurationMinutes = lesson.DurationMinutes,
                Sequence = lesson.Sequence,
                HasQuiz = lesson.HasQuiz,
                HasPractice = lesson.HasPractice,
                CreatedAt = lesson.CreatedAt,
            };

            var added = await _lessonRepository.AddNewLesson(newLesson);
            return Result<Lesson>.Success(added, "Added successfully");
        }

        public async Task<Result<IEnumerable<Lesson>>> GetAllLessonsAsync()
        {
            var list = await _lessonRepository.GetAllLessonsAsync();
            return Result<IEnumerable<Lesson>>.Success(list);
        }

        public async Task<Result<Lesson>> GetLessonByIdAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetLessonByIdAsync(id);
            if (lesson == null) return Result<Lesson>.NotFound($"Not found Lesson with id: {id}");
            return Result<Lesson>.Success(lesson);
        }

        public async Task<Result<bool>> DeleteLessonByIdAsync(Guid id)
        {
            await _lessonRepository.DeleteLessonByIdAsync(id);
            return Result<bool>.Success(true, "Deleted successfully");
        }

        public async Task<Result<Lesson>> UpdateLessonAsync(Guid id, Lesson lesson)
        {
            var existing = await _lessonRepository.GetLessonByIdAsync(id);
            if (existing == null) return Result<Lesson>.NotFound($"Not found Lesson with id: {id}");
            // Update fields
            existing.Title = lesson.Title;
            existing.WeekId = lesson.WeekId;
            existing.DurationMinutes = lesson.DurationMinutes;
            existing.Sequence = lesson.Sequence;
            existing.HasQuiz = lesson.HasQuiz;
            existing.HasPractice = lesson.HasPractice;
            await _lessonRepository.UpdateLessonAsync(existing);
            return Result<Lesson>.Success(existing, "Updated successfully");
        }
    }
} 