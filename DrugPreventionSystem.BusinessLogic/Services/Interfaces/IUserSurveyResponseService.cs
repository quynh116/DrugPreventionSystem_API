using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.UserSurveyResponse;
using DrugPreventionSystem.BusinessLogic.Models.Responses;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserSurveyResponseService
    {
        Task<Result<IEnumerable<UserSurveyResponseResponse>>> GetAllAsync();
        Task<Result<UserSurveyResponseResponse>> GetByIdAsync(Guid id);
        Task<Result<UserSurveyResponseResponse>> CreateAsync(UserSurveyResponseCreateRequest request);
        Task<Result<UserSurveyResponseResponse>> UpdateAsync(UserSurveyResponseUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);


    }
}
