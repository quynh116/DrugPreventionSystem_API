using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.BusinessLogic.Services.Quizzes;
using DrugPreventionSystem.DataAccess.Repository.Interfaces.IQuizzes;
using DrugPreventionSystem.DataAccess.Repository.Quizzes;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IUserLessonProgressRepository _userLessonProgressRepository;
        private readonly IUserCourseEnrollmentRepository _userCourseEnrollmentRepository;

        public LessonService(ILessonRepository lessonRepository, IUserLessonProgressRepository userLessonProgressRepository,
        IUserCourseEnrollmentRepository userCourseEnrollmentRepository, IQuizRepository quizRepository)
        {
            _userLessonProgressRepository = userLessonProgressRepository;
            _userCourseEnrollmentRepository = userCourseEnrollmentRepository;
            _lessonRepository = lessonRepository;
            _quizRepository = quizRepository;
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

        public async Task<Result<LessonDetailResponse>> GetLessonDetailsForUserAsync(Guid lessonId, Guid userId)
        {
            try
            {
                var lesson = await _lessonRepository.GetLessonByIdAsync(lessonId);

                if (lesson == null)
                {
                    return Result<LessonDetailResponse>.NotFound($"Lesson with ID {lessonId} not found.");
                }

                if (lesson.CourseWeek?.Course == null)
                {
                    return Result<LessonDetailResponse>.Error("Course or CourseWeek information missing for this lesson.");
                }

                var course = lesson.CourseWeek.Course;

                


                
                var videoResource = lesson.LessonResources
                                          .FirstOrDefault(lr => lr.ResourceType.Equals("video", StringComparison.OrdinalIgnoreCase));

                
                int totalLessonsInCourse = course.CourseWeeks?.SelectMany(cw => cw.Lessons).Count() ?? 0;

                
                var userProgressesInCourse = await _userLessonProgressRepository.GetUserLessonProgressByUserIdAndCourseIdAsync(userId, course.CourseId);
                int completedLessonsByUser = userProgressesInCourse.Count(ulp => ulp.Passed);

                float courseProgressPercentage = totalLessonsInCourse > 0 ? (float)completedLessonsByUser / totalLessonsInCourse * 100 : 0;

                
                var currentUserLessonProgress = userProgressesInCourse.FirstOrDefault(ulp => ulp.LessonId == lessonId);
                bool isLessonCompleted = currentUserLessonProgress?.Passed ?? false;

                var response = new LessonDetailResponse
                {
                    LessonId = lesson.LessonId,
                    Title = lesson.Title,
                    DurationMinutes = lesson.DurationMinutes,
                    VideoUrl = videoResource?.ResourceUrl,
                    Description = videoResource?.Description, 
                    CourseTitle = course.Title,
                    CourseId = course.CourseId,
                    CourseProgressPercentage = courseProgressPercentage,
                    IsCompleted = isLessonCompleted,
                    
                    
                };

                return Result<LessonDetailResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<LessonDetailResponse>.Error($"Error getting lesson details: {ex.Message}");
            }
        }

        public async Task<Result<QuestionAndOptionResponse>> GetQuizQuestionsAndAnswersByLessonIdAsync(Guid lessonId)
        {
            var quiz = await _quizRepository.GetQuizByLessonIdAsync(lessonId);

            if (quiz == null)
            {
                return Result<QuestionAndOptionResponse>.NotFound($"Quiz not found for Lesson with ID {lessonId}.");
            }

            var quizResponse = new QuestionAndOptionResponse
            {
                QuizId = quiz.QuizId,
                LessonId = quiz.LessonId,
                Questions = quiz.QuizQuestions.Select(qq => new QuizQuestionResponse1
                {
                    QuestionId = qq.QuestionId,
                    QuestionText = qq.QuestionText,
                    QuestionType = qq.QuestionType,
                    Sequence = qq.Sequence,
                    Options = qq.QuizOptions.Select(qo => new QuizOptionResponse1
                    {
                        OptionId = qo.OptionId,
                        OptionText = qo.OptionText,
                        IsCorrect = qo.IsCorrect // Bao g?m c? IsCorrect ?? tr? l?i
                    }).ToList()
                }).ToList()
            };

            return Result<QuestionAndOptionResponse>.Success(quizResponse);
        }
    }
} 