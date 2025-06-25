using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Models.Responses.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Commons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IProgramFeedbackService
    {
        Task<Result<ProgramFeedbackResponse>> GetByIdAsync(Guid feedbackId);
        Task<Result<List<ProgramFeedbackResponse>>> GetAllAsync();
        Task<Result<ProgramFeedbackResponse>> CreateAsync(ProgramFeedbackCreateRequest request);
        Task<Result<ProgramFeedbackResponse>> UpdateAsync(Guid feedbackId, ProgramFeedbackUpdateRequest request);
        Task<Result<bool>> DeleteAsync(Guid feedbackId);
    }
} 