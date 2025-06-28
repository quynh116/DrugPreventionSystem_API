using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Lesson;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Lesson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Services.Quizzes;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ILessonService
    {
        Task<Result<Lesson>> AddNewLessonAsync(LessonRequest lesson);
        Task<Result<IEnumerable<LessonResponse>>> GetAllLessonsAsync();
        Task<Result<LessonResponse>> GetLessonByIdAsync(Guid id);
        Task<Result<bool>> DeleteLessonByIdAsync(Guid id);
        Task<Result<Lesson>> UpdateLessonAsync(Guid id, Lesson lesson);
        Task<Result<LessonDetailResponse>> GetLessonDetailsForUserAsync(Guid lessonId, Guid userId);
        Task<Result<QuestionAndOptionResponse>> GetQuizQuestionsAndAnswersByLessonIdAsync(Guid lessonId);

        Task<Result<QuizResultResponse>> SubmitQuizAttemptAsync(SubmitQuizRequest request);
        Task<Result<QuizResultResponse>> GetUserQuizResultForLessonAsync(Guid userId, Guid lessonId);
        Task<Result<bool>> CompleteLessonAsync(CompleteLessonRequest request);
        Task<Result<QuizStatusResponse>> GetQuizInitialStateForUserAsync(Guid lessonId, Guid userId,bool forceAttempt = false);
    }
}