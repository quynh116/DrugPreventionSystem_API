using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant;
using DrugPreventionSystem.BusinessLogic.Models.Responses;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IProgramParticipantService
    {
        Task<Result<ProgramParticipantResponse>> CreateAsync(ProgramParticipantCreateRequest request);
        Task<Result<IEnumerable<ProgramParticipantResponse>>> GetAllAsync();
        Task<Result<ProgramParticipantResponse>> GetByIdAsync(Guid id);
        Task<Result<ProgramParticipantResponse>> UpdateAsync(ProgramParticipantUpdateRequest request, Guid id);
        Task<Result<bool>> DeleteAsync(Guid id);
    }
}
