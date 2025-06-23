using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.Quizzes.QuizOption;
using DrugPreventionSystem.BusinessLogic.Models.Responses.Quiz;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces.IQuizzes;

namespace DrugPreventionSystem.BusinessLogic.Services.Quizzes
{
    public class QuizOptionService : IQuizOptionService
    {
        private readonly IQuizOptionRepository _quizOptionRepository;
        private readonly ProvideToken _provideToken;

        public QuizOptionService(IQuizOptionRepository quizOptionRepository, ProvideToken provideToken)
        {
            _quizOptionRepository = quizOptionRepository;
            _provideToken = provideToken;
        }

        private QuizOptionResponse MapToResponse(QuizOption option)
        {
            if (option == null)
            {
                return null;
            }

            return new QuizOptionResponse()
            {
                OptionId = option.OptionId,
                QuestionId = option.QuestionId,
                OptionText = option.OptionText,
                IsCorrect = option.IsCorrect,
                CreatedAt = option.CreatedAt
            };
        }

        public async Task<Result<QuizOptionResponse>> CreateAsync(QuizOptionCreateRequest request)
        {
            var newOption = new QuizOption()
            {
                OptionId = Guid.NewGuid(),
                QuestionId = request.QuestionId,
                OptionText = request.OptionText,
                IsCorrect = request.IsCorrect,
                CreatedAt = DateTime.Now
            };

            var createdOption = await _quizOptionRepository.CreateAsync(newOption);
            return Result<QuizOptionResponse>.Success(MapToResponse(createdOption));
        }

        public async Task<Result<IEnumerable<QuizOptionResponse>>> GetAllAsync()
        {
            try
            {
                var options = await _quizOptionRepository.GetAllAsync();
                var responses = options.Select(o => MapToResponse(o)).ToList();
                return Result<IEnumerable<QuizOptionResponse>>.Success(responses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<QuizOptionResponse>>.Error($"Error getting options: {ex.Message}");
            }
        }

        public async Task<Result<QuizOptionResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var option = await _quizOptionRepository.GetByIdAsync(id);
                if (option == null)
                {
                    return Result<QuizOptionResponse>.NotFound($"Option with ID {id} not found.");
                }

                return Result<QuizOptionResponse>.Success(MapToResponse(option));
            }
            catch (Exception ex)
            {
                return Result<QuizOptionResponse>.Error($"Error getting option: {ex.Message}");
            }
        }

        public async Task<Result<QuizOptionResponse>> UpdateAsync(QuizOptionUpdateRequest request, Guid id)
        {
            try
            {
                var option = await _quizOptionRepository.GetByIdAsync(id);
                if (option == null)
                {
                    return Result<QuizOptionResponse>.NotFound($"Option with ID {id} not found.");
                }

                option.OptionText = request.OptionText;
                option.IsCorrect = request.IsCorrect;

                await _quizOptionRepository.UpdateAsync(option);
                return Result<QuizOptionResponse>.Success(MapToResponse(option));
            }
            catch (Exception ex)
            {
                return Result<QuizOptionResponse>.Error($"Error updating option: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var option = await _quizOptionRepository.GetByIdAsync(id);
            try
            {
                if (option == null)
                {
                    return Result<bool>.NotFound($"Option with ID {id} not found.");
                }

                await _quizOptionRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Option deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting option: {ex.Message}");
            }
        }
    }

}
