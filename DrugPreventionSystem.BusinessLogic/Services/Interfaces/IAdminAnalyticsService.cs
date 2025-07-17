using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IAdminAnalyticsService
    {
        Task<Result<AdminDashboardSummaryDto>> GetAdminDashboardSummaryAsync();
    }
}
