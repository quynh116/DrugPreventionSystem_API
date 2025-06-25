using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.Quiz;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces.IQuizzes;

namespace DrugPreventionSystem.BusinessLogic.Services.Quizzes
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ProvideToken _provideToken;

        public QuizService(IQuizRepository quizRepository, ProvideToken provideToken)
        {
            _quizRepository = quizRepository;
            _provideToken = provideToken;
        }

        private QuizResponse MapToResponse(Quiz quiz)
        {
            if (quiz == null)
            {
                return null;
            }
            return new QuizResponse()
            {
                QuizId = quiz.QuizId,
                LessonId = quiz.LessonId,
                Title = quiz.Title,
                Description = quiz.Description,
                TotalQuestions = quiz.TotalQuestions,
                PassingScore = quiz.PassingScore,
                CreatedAt = quiz.CreatedAt
            };
        }

        public async Task<Result<QuizResponse>> CreateAsync(QuizCreateRequest request)
        {
            var newQuiz = new Quiz()
            {
                QuizId = Guid.NewGuid(),
                LessonId = request.LessonId,
                Title = request.Title,
                Description = request.Description,
                TotalQuestions = request.TotalQuestions,
                PassingScore = request.PassingScore,
                CreatedAt = DateTime.Now
            };

            var createdQuiz = await _quizRepository.CreateAsync(newQuiz);
            return Result<QuizResponse>.Success(MapToResponse(createdQuiz));
        }

        public async Task<Result<IEnumerable<QuizResponse>>> GetAllAsync()
        {
            try
            {
                var quizzes = await _quizRepository.GetAllAsync();
                var quizResponses = quizzes.Select(q => MapToResponse(q)).ToList();
                return Result<IEnumerable<QuizResponse>>.Success(quizResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<QuizResponse>>.Error($"Error getting quizzes: {ex.Message}");
            }
        }

        public async Task<Result<QuizResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                if (quiz == null)
                {
                    return Result<QuizResponse>.NotFound($"Quiz with ID {id} not found.");
                }
                return Result<QuizResponse>.Success(MapToResponse(quiz));
            }
            catch (Exception ex)
            {
                return Result<QuizResponse>.Error($"Error getting quiz: {ex.Message}");
            }
        }

        public async Task<Result<QuizResponse>> UpdateAsync(QuizUpdateRequest request, Guid id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                if (quiz == null)
                {
                    return Result<QuizResponse>.NotFound($"Quiz with ID {id} not found.");
                }

                quiz.Title = request.Title;
                quiz.Description = request.Description;
                quiz.TotalQuestions = request.TotalQuestions;
                quiz.PassingScore = request.PassingScore;

                await _quizRepository.UpdateAsync(quiz);
                return Result<QuizResponse>.Success(MapToResponse(quiz));
            }
            catch (Exception ex)
            {
                return Result<QuizResponse>.Error($"Error updating quiz: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            try
            {
                if (quiz == null)
                {
                    return Result<bool>.NotFound($"Quiz with ID {id} not found.");
                }
                await _quizRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Quiz deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting quiz: {ex.Message}");
            }
        }
    }

}
