using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Models.Responses.ProgramFeedback;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IProgramService
    {
        Task<ProgramParticipantResponse> RegisterForProgramAsync(ProgramParticipantCreateRequest request);
        Task<IEnumerable<CommunityProgramResponse>> GetProgramsUserEnrolledAsync(Guid userId);
        Task<ProgramFeedbackResponse> SubmitProgramFeedbackAsync(ProgramFeedbackCreateRequest request);
        Task<bool> CancelRegistrationAsync(ProgramParticipantCancelRequest request);
    }
} 