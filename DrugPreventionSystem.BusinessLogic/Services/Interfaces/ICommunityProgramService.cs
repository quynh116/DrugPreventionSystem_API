using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface ICommunityProgramService
    {
        Task<Result<CommunityProgramResponse>> GetProgramById(Guid id);
        Task<Result<IEnumerable<CommunityProgramResponse>>> GetAllPrograms();
        Task<Result<CommunityProgramResponse>> AddCommunityProgram(CommunityProgramCreateRequest request);
        Task<Result<CommunityProgramResponse>> UpdateCommunityProgram(CommunityProgramUpdateRequest program, Guid programId);
        Task<Result<bool>> DeleteCommunityProgram(Guid communityProgramId);
    }
}
