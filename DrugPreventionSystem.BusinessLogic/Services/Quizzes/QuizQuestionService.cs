using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizQuestion;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces.IQuizzes;
using DrugPreventionSystem.DataAccess.Repository.Quizzes;

namespace DrugPreventionSystem.BusinessLogic.Services.Quizzes
{
    public class QuizQuestionService : IQuizQuestionService
    {
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly ProvideToken _provideToken;

        public QuizQuestionService(IQuizQuestionRepository quizQuestionRepository, ProvideToken provideToken)
        {
            _quizQuestionRepository = quizQuestionRepository;
            _provideToken = provideToken;
        }

        private QuizQuestionResponse MapToResponse(QuizQuestion question)
        {
            if (question == null)
            {
                return null;
            }

            return new QuizQuestionResponse()
            {
                QuestionId = question.QuestionId,
                QuizId = question.QuizId,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                Sequence = question.Sequence,
                CreatedAt = question.CreatedAt
            };
        }

        public async Task<Result<QuizQuestionResponse>> CreateAsync(QuizQuestionCreateRequest request)
        {
            var newQuestion = new QuizQuestion()
            {
                QuestionId = Guid.NewGuid(),
                QuizId = request.QuizId,
                QuestionText = request.QuestionText,
                QuestionType = request.QuestionType,
                Sequence = request.Sequence,
                CreatedAt = DateTime.Now
            };

            var created = await _quizQuestionRepository.CreateAsync(newQuestion);
            return Result<QuizQuestionResponse>.Success(MapToResponse(created));
        }

        public async Task<Result<IEnumerable<QuizQuestionResponse>>> GetAllAsync()
        {
            try
            {
                var questions = await _quizQuestionRepository.GetAllAsync();
                var responses = questions.Select(q => MapToResponse(q)).ToList();
                return Result<IEnumerable<QuizQuestionResponse>>.Success(responses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<QuizQuestionResponse>>.Error($"Error getting questions: {ex.Message}");
            }
        }

        public async Task<Result<QuizQuestionResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var question = await _quizQuestionRepository.GetByIdAsync(id);
                if (question == null)
                {
                    return Result<QuizQuestionResponse>.NotFound($"Question with ID {id} not found.");
                }

                return Result<QuizQuestionResponse>.Success(MapToResponse(question));
            }
            catch (Exception ex)
            {
                return Result<QuizQuestionResponse>.Error($"Error getting question: {ex.Message}");
            }
        }

        public async Task<Result<QuizQuestionResponse>> UpdateAsync(QuizQuestionUpdateRequest request, Guid id)
        {
            try
            {
                var question = await _quizQuestionRepository.GetByIdAsync(id);
                if (question == null)
                {
                    return Result<QuizQuestionResponse>.NotFound($"Question with ID {id} not found.");
                }

                question.QuestionText = request.QuestionText;
                question.QuestionType = request.QuestionType;
                question.Sequence = request.Sequence;

                await _quizQuestionRepository.UpdateAsync(question);
                return Result<QuizQuestionResponse>.Success(MapToResponse(question));
            }
            catch (Exception ex)
            {
                return Result<QuizQuestionResponse>.Error($"Error updating question: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var question = await _quizQuestionRepository.GetByIdAsync(id);
            try
            {
                if (question == null)
                {
                    return Result<bool>.NotFound($"Question with ID {id} not found.");
                }

                await _quizQuestionRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Question deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting question: {ex.Message}");
            }
        }
    }


}
