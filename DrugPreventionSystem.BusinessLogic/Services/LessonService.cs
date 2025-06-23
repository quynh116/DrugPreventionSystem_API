using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;

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

        public async Task<Result<IEnumerable<LessonResponse>>> GetAllLessonsAsync()
        {
            var list = await _lessonRepository.GetAllLessonsAsync();
            var responseList = new List<LessonResponse>();
            foreach (var l in list)
            {
                responseList.Add(MapLessonToResponse(l));
            }
            return Result<IEnumerable<LessonResponse>>.Success(responseList);
        }

        public async Task<Result<LessonResponse>> GetLessonByIdAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetLessonByIdAsync(id);
            if (lesson == null) return Result<LessonResponse>.NotFound($"Not found Lesson with id: {id}");
            return Result<LessonResponse>.Success(MapLessonToResponse(lesson));
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

        private LessonResponse MapLessonToResponse(Lesson l)
        {
            return new LessonResponse
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
            };
        }
    }
} 