using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Models.Responses.ProgramFeedback;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IProgramService
    {
        Task<ProgramParticipantResponse> RegisterForProgramAsync(ProgramParticipantCreateRequest request);
        Task<IEnumerable<CommunityProgramResponse>> GetProgramsUserEnrolledAsync(Guid userId);
        Task<ProgramFeedbackResponse> SubmitProgramFeedbackAsync(ProgramFeedbackCreateRequest request);
        Task<bool> CancelRegistrationAsync(ProgramParticipantCancelRequest request);
        Task<ProgramSurveyWithUserResponseDto?> GetProgramSurveyAsync(Guid programId);
        Task<bool> CanUserTakeSurveyAsync(Guid userId, Guid programId);
        Task<ProgramSurveyResponseDto> SubmitProgramSurveyAsync(Guid userId, Guid programId, SubmitProgramSurveyDto surveyDto);
        Task<ProgramSurveyResponseDto?> GetUserProgramSurveyResponseAsync(Guid userId, Guid programId);

        Task<ProgramParticipant?> GetUserProgramParticipationStatusAsync(Guid userId, Guid programId);
    }
}