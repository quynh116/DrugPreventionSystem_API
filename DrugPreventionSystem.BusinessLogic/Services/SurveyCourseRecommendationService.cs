using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Models.Responses.SurveyCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class SurveyCourseRecommendationService : ISurveyCourseRecommendationService
    {
        private readonly ISurveyCourseRecommendationRepository _surveyCourseRecommendationRepository;

        public SurveyCourseRecommendationService(ISurveyCourseRecommendationRepository surveyCourseRecommendationRepository)
        {
            _surveyCourseRecommendationRepository = surveyCourseRecommendationRepository;
        }
        private SurveyCourseRecommendationResponse MapToSurveyCourseRecommendationResponse(SurveyCourseRecommendation recommendation)
        {
            if (recommendation == null)
            {
                return null;
            }
            return new SurveyCourseRecommendationResponse
            {
                SurveyId = recommendation.SurveyId,
                RiskLevel = recommendation.RiskLevel,
                RecommendedActionKeyword = recommendation.RecommendedActionKeyword,
                CourseId = recommendation.CourseId,
                Priority = recommendation.Priority,
                CreatedAt = recommendation.CreatedAt,
                UpdatedAt = recommendation.UpdatedAt
            };
        }
        public async Task<Result<SurveyCourseRecommendationResponse>> AddRecommendationAsync(SurveyCourseRecommendationCreateRequest request)
        {
            try
            {
                if (request.SurveyId == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("SurveyId cannot be null");
                }
                if (request.CourseId == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("CourseId cannot be null");
                }
                if (string.IsNullOrEmpty(request.RiskLevel))
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("RiskLevel cannot be null or empty");
                }
                if (request.RecommendedActionKeyword == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("RecommendedActionKeyword cannot be null");
                }
                var recommendation = new SurveyCourseRecommendation()
                {
                    SurveyId = request.SurveyId,
                    RiskLevel = request.RiskLevel,
                    RecommendedActionKeyword = request.RecommendedActionKeyword,
                    CourseId = request.CourseId,
                    Priority = request.Priority,
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt
                };
                var addedRecommendation = await _surveyCourseRecommendationRepository.AddRecommendation(recommendation);
                return Result<SurveyCourseRecommendationResponse>.Success(MapToSurveyCourseRecommendationResponse(addedRecommendation), "Adding successfully");

            }
            catch (Exception ex)
            {
                return Result<SurveyCourseRecommendationResponse>.Error($"Error adding recommendation: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<SurveyCourseRecommendationResponse>>> GetAllSurveyCourseRecommendationAsync()
        {
            try
            {
                var result = await _surveyCourseRecommendationRepository.GetAllCourseRecommendations();
                if (result == null || !result.Any())
                {
                    return Result<IEnumerable<SurveyCourseRecommendationResponse>>.NotFound("No recommendations found");
                }
                var recommendations = result.Select(r => MapToSurveyCourseRecommendationResponse(r)).ToList();
                return Result<IEnumerable<SurveyCourseRecommendationResponse>>.Success(recommendations, "Recommendations retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SurveyCourseRecommendationResponse>>.Error($"Error retrieving recommendations: {ex.Message}");
            }
        }

        public async Task<Result<SurveyCourseRecommendationResponse>> GetRecommendationByIdAsync(Guid recommendationId)
        {
            try
            {
                var recommendation = await _surveyCourseRecommendationRepository.GetRecommendationById(recommendationId);
                if (recommendation == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.NotFound($"Recommendation with ID {recommendationId} not found");
                }
                return Result<SurveyCourseRecommendationResponse>.Success(MapToSurveyCourseRecommendationResponse(recommendation), "Recommendation retrieved successfully");
            }
            catch
            {
                return Result<SurveyCourseRecommendationResponse>.Error($"Error retrieving recommendation with ID {recommendationId}");
            }
        }

        public async Task<Result<IEnumerable<SurveyCourseRecommendationResponse>>> GetRecommendationsByCourseIdAsync(Guid courseId)
        {
            try
            {
                var recommendations = await _surveyCourseRecommendationRepository.GetRecommendationsByCourseId(courseId);
                if (recommendations == null || !recommendations.Any())
                {
                    return Result<IEnumerable<SurveyCourseRecommendationResponse>>.NotFound($"No recommendations found for course ID {courseId}");
                }
                var response = recommendations.Select(r => MapToSurveyCourseRecommendationResponse(r)).ToList();
                return Result<IEnumerable<SurveyCourseRecommendationResponse>>.Success(response, "Recommendations retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SurveyCourseRecommendationResponse>>.Error($"Error retrieving recommendations for course ID {courseId}: {ex.Message}");
            }
        }

        public async Task<Result<SurveyCourseRecommendationResponse>> UpdateRecommendationAsync(Guid recommendationId, SurveyCourseRecommendationUpdateRequest request)
        {
            try
            {
                var recommendation = await _surveyCourseRecommendationRepository.GetRecommendationById(recommendationId);
                if (recommendation == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.NotFound($"Recommendation with ID {recommendationId} not found");
                }
                if (request.SurveyId == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("SurveyId cannot be null");
                }
                if (request.CourseId == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("CourseId cannot be null");
                }
                if (string.IsNullOrEmpty(request.RiskLevel))
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("RiskLevel cannot be null or empty");
                }
                if (request.RecommendedActionKeyword == null)
                {
                    return Result<SurveyCourseRecommendationResponse>.Invalid("RecommendedActionKeyword cannot be null");
                }
                recommendation.SurveyId = request.SurveyId;
                recommendation.RiskLevel = request.RiskLevel;
                recommendation.RecommendedActionKeyword = request.RecommendedActionKeyword;
                recommendation.CourseId = request.CourseId;
                recommendation.Priority = request.Priority;
                recommendation.UpdatedAt = DateTime.Now;
                var updatedRecommendation = await _surveyCourseRecommendationRepository.UpdateRecommendation(recommendation);
                return Result<SurveyCourseRecommendationResponse>
                    .Success(MapToSurveyCourseRecommendationResponse(updatedRecommendation), "Recommendation updated successfully");
            }
            catch (Exception ex)
            {
                return Result<SurveyCourseRecommendationResponse>.Error($"Error updating recommendation: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteRecommendationAsync(Guid recommendationId)
        {
            try
            {
                var recommendation = _surveyCourseRecommendationRepository.GetRecommendationById(recommendationId);
                if (recommendation == null)
                {
                    return Result<bool>.NotFound($"Recommendation with ID {recommendationId} not found");
                }
                await _surveyCourseRecommendationRepository.DeleteRecommendation(recommendationId);
                return Result<bool>.Success(true, $"Recommendation with ID {recommendationId} deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting recommendation with ID {recommendationId}: {ex.Message}");
            }
        }
    }
}
