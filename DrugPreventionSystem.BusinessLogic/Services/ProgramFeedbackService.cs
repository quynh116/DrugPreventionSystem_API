using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Models.Responses.ProgramFeedback;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramFeedbackService : IProgramFeedbackService
    {
        private readonly IProgramFeedbackRepository _repository;
        public ProgramFeedbackService(IProgramFeedbackRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProgramFeedbackResponse>> GetByIdAsync(Guid feedbackId)
        {
            var feedback = await _repository.GetByIdAsync(feedbackId);
            if (feedback == null) return Result<ProgramFeedbackResponse>.NotFound("Feedback not found");
            return Result<ProgramFeedbackResponse>.Success(MapToResponse(feedback));
        }

        public async Task<Result<List<ProgramFeedbackResponse>>> GetAllAsync()
        {
            var feedbacks = await _repository.GetAllAsync();
            return Result<List<ProgramFeedbackResponse>>.Success(feedbacks.Select(MapToResponse).ToList());
        }

        public async Task<Result<ProgramFeedbackResponse>> CreateAsync(ProgramFeedbackCreateRequest request)
        {
            var feedback = new ProgramFeedback
            {
                FeedbackId = Guid.NewGuid(),
                ProgramId = request.ProgramId,
                UserId = request.UserId,
                Rating = request.Rating,
                Comments = request.Comments,
                SubmittedAt = DateTime.Now
            };
            await _repository.AddAsync(feedback);
            return Result<ProgramFeedbackResponse>.Success(MapToResponse(feedback));
        }

        public async Task<Result<ProgramFeedbackResponse>> UpdateAsync(Guid feedbackId, ProgramFeedbackUpdateRequest request)
        {
            var feedback = await _repository.GetByIdAsync(feedbackId);
            if (feedback == null) return Result<ProgramFeedbackResponse>.NotFound("Feedback not found");
            feedback.Rating = request.Rating;
            feedback.Comments = request.Comments;
            await _repository.UpdateAsync(feedback);
            return Result<ProgramFeedbackResponse>.Success(MapToResponse(feedback));
        }

        public async Task<Result<bool>> DeleteAsync(Guid feedbackId)
        {
            var feedback = await _repository.GetByIdAsync(feedbackId);
            if (feedback == null) return Result<bool>.NotFound("Feedback not found");
            await _repository.DeleteAsync(feedbackId);
            return Result<bool>.Success(true);
        }

        private ProgramFeedbackResponse MapToResponse(ProgramFeedback feedback)
        {
            return new ProgramFeedbackResponse
            {
                FeedbackId = feedback.FeedbackId,
                ProgramId = feedback.ProgramId,
                UserId = feedback.UserId,
                Rating = feedback.Rating,
                Comments = feedback.Comments,
                SubmittedAt = feedback.SubmittedAt
            };
        }
    }
} 