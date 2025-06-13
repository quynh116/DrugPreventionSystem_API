using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<Result<IEnumerable<UserProfileResponse>>> GetAllUsersProfileAsync();
        Task<Result<UserProfileResponse>> GetUserProfileByIdAsync(Guid id);
        Task<Result<UserProfileResponse>> GetUserProfileByUserIdAsync(Guid userId);
        Task<Result<UserProfileResponse>> UpdateProfileUserAsync(Guid id, UserProfileUpdateRequest request);
        Task<Result<bool>> DeleteUserProfileAsync(Guid id);
    }
}
