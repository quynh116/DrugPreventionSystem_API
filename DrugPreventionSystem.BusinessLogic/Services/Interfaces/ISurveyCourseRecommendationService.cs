using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.SurveyCourseRecommendation;
using DrugPreventionSystem.BusinessLogic.Models.Responses.SurveyCourseRecommendation;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ISurveyCourseRecommendationService
    {
        Task<Result<IEnumerable<SurveyCourseRecommendationResponse>>> GetAllSurveyCourseRecommendationAsync();
        Task<Result<SurveyCourseRecommendationResponse>> GetRecommendationByIdAsync(Guid recommendationId);
        Task<Result<IEnumerable<SurveyCourseRecommendationResponse>>> GetRecommendationsByCourseIdAsync(Guid courseId);
        Task<Result<SurveyCourseRecommendationResponse>> AddRecommendationAsync(SurveyCourseRecommendationCreateRequest request);
        Task<Result<SurveyCourseRecommendationResponse>> UpdateRecommendationAsync(Guid recommendationId, SurveyCourseRecommendationUpdateRequest request);
        Task<Result<bool>> DeleteRecommendationAsync(Guid recommendationId);
    }
}
