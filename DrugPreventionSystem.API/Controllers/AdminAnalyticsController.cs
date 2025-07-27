using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugPreventionSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAnalyticsController : BaseApiController
    {
        private readonly IAdminAnalyticsService _adminAnalyticsService; 

        public AdminAnalyticsController(IAdminAnalyticsService adminAnalyticsService)
        {
            _adminAnalyticsService = adminAnalyticsService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<Result<AdminDashboardSummaryDto>>> GetAdminDashboardSummary()
        {
            var result = await _adminAnalyticsService.GetAdminDashboardSummaryAsync();
            return HandleResult(result); 
        }
        [HttpGet("summary_home")]
        public async Task<ActionResult<Result<DashboardSummaryDto>>> GetDashboardSummary()
        {
            var result = await _adminAnalyticsService.GetDashboardSummaryAsync();
            return HandleResult(result);
        }
    }
}
