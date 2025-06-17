using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyResponse;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserSurveyResponseService :IUserSurveyResponseService
    {
        private readonly IUserSurveyResponseRepository _userSurveyResponseRepository;
        private readonly ProvideToken _provideToken;

        public UserSurveyResponseService(IUserSurveyResponseRepository userSurveyResponseRepository, ProvideToken provideToken)
        {
            _userSurveyResponseRepository = userSurveyResponseRepository;
            _provideToken = provideToken;

        }

        public UserSurveyResponseResponse MapToResponse(UserSurveyResponse user)
        {
            return new UserSurveyResponseResponse
            {
                ResponseId = user.ResponseId,
                UserId = user.UserId,
                SurveyId = user.SurveyId,
                TakenAt = user.TakenAt,
                RiskLevel = user.RiskLevel,
                RecommendedAction = user.RecommendedAction,
            };
        }

        public async Task<Result<IEnumerable<UserSurveyResponseResponse>>> GetAllAsync()
        {
            try
            {
                var result = await _userSurveyResponseRepository.GetAllAsync();
                var response = result.Select(r => MapToResponse(r)).ToList();
                return Result<IEnumerable<UserSurveyResponseResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserSurveyResponseResponse>>.Error($"Error retrieving users: {ex.Message}");
            }

        }
        public async Task<Result<UserSurveyResponseResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _userSurveyResponseRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return Result<UserSurveyResponseResponse>.NotFound($"UserSurveyResponse with ID {id} not found.");
                }
                return Result<UserSurveyResponseResponse>.Success(MapToResponse(result));
            }
            catch (Exception ex)
            {
                return Result<UserSurveyResponseResponse>.Error($"Error retrieving users: {ex.Message}");
            }
        }
        public async Task<Result<UserSurveyResponseResponse>> CreateAsync(UserSurveyResponseCreateRequest request)
        {
            var newResponse = new UserSurveyResponse()
            {
                ResponseId = Guid.NewGuid(),
                UserId = request.UserId,
                SurveyId = request.SurveyId,
                RiskLevel = request.RiskLevel,
                RecommendedAction = request.RecommendedAction,
            };
            var createResonpse = await _userSurveyResponseRepository.CreateAsync(newResponse);
            return Result<UserSurveyResponseResponse>.Success(MapToResponse(createResonpse));
        }

        public async Task<Result<UserSurveyResponseResponse>> UpdateAsync(UserSurveyResponseUpdateRequest request, Guid id)
        {
            try
            {
                var response = await _userSurveyResponseRepository.GetByIdAsync(id);
                if (response == null)
                {
                    return Result<UserSurveyResponseResponse>.NotFound($"Survey response with ID {id} not found.");
                }
                response.TakenAt = request.TakenAt;
                response.RiskLevel = request.RiskLevel;
                response.RecommendedAction = request.RecommendedAction;
                await _userSurveyResponseRepository.UpdateAsync(response);
                return Result<UserSurveyResponseResponse>.Success(MapToResponse(response));

            }
            catch (Exception ex)
            {
                {
                    return Result<UserSurveyResponseResponse>.Error($"Error updating survey response: {ex.Message}");
                }

            }
        }
        public async Task<Result<bool>>DeleteAsync(Guid id)
        {
            try
            {
                var response = await _userSurveyResponseRepository.GetByIdAsync(id);
                if (response == null)
                {
                    return Result<bool>.NotFound($"Survey response with ID {id} not found.");
                }
                await _userSurveyResponseRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Survey response deleted successfully.");
            }
            catch (Exception ex)
            {
                {
                    return Result<bool>.Error($"Error updating survey response: {ex.Message}");
                }

            }
        }
    }
}
