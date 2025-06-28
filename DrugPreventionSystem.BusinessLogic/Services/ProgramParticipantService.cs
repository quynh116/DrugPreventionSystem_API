using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramParticipantService : IProgramParticipantService
    {
        private readonly IProgramParticipantRepository _participantRepository;

        public ProgramParticipantService(IProgramParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        private ProgramParticipantResponse MapToResponse(ProgramParticipant participant)
        {
            if (participant == null) return null;

            return new ProgramParticipantResponse
            {
                ParticipantId = participant.ParticipantId,
                ProgramId = participant.ProgramId,
                UserId = participant.UserId,
                RegisteredAt = participant.RegisteredAt,
                Attended = participant.Attended,
                FeedbackSubmitted = participant.FeedbackSubmitted
            };
        }

        public async Task<Result<ProgramParticipantResponse>> CreateAsync(ProgramParticipantCreateRequest request)
        {
            var newParticipant = new ProgramParticipant
            {
                ParticipantId = Guid.NewGuid(),
                ProgramId = request.ProgramId,
                UserId = request.UserId,
                RegisteredAt = request.RegisteredAt,
                Attended = request.Attended,
                FeedbackSubmitted = request.FeedbackSubmitted
            };

            var created = await _participantRepository.CreateAsync(newParticipant);
            return Result<ProgramParticipantResponse>.Success(MapToResponse(created));
        }

        public async Task<Result<IEnumerable<ProgramParticipantResponse>>> GetAllAsync()
        {
            try
            {
                var participants = await _participantRepository.GetAllAsync();
                var responses = participants.Select(MapToResponse).ToList();
                return Result<IEnumerable<ProgramParticipantResponse>>.Success(responses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ProgramParticipantResponse>>.Error($"Error retrieving participants: {ex.Message}");
            }
        }

        public async Task<Result<ProgramParticipantResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var participant = await _participantRepository.GetByIdAsync(id);
                if (participant == null)
                    return Result<ProgramParticipantResponse>.NotFound($"Participant with ID {id} not found.");

                return Result<ProgramParticipantResponse>.Success(MapToResponse(participant));
            }
            catch (Exception ex)
            {
                return Result<ProgramParticipantResponse>.Error($"Error retrieving participant: {ex.Message}");
            }
        }

        public async Task<Result<ProgramParticipantResponse>> UpdateAsync(ProgramParticipantUpdateRequest request, Guid id)
        {
            try
            {
                var participant = await _participantRepository.GetByIdAsync(id);
                if (participant == null)
                    return Result<ProgramParticipantResponse>.NotFound($"Participant with ID {id} not found.");

                participant.Attended = request.Attended;
                participant.FeedbackSubmitted = request.FeedbackSubmitted;

                await _participantRepository.UpdateAsync(participant);
                return Result<ProgramParticipantResponse>.Success(MapToResponse(participant));
            }
            catch (Exception ex)
            {
                return Result<ProgramParticipantResponse>.Error($"Error updating participant: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            try
            {
                if (participant == null)
                    return Result<bool>.NotFound($"Participant with ID {id} not found.");

                await _participantRepository.DeleteAsync(id);
                return Result<bool>.Success(true, "Participant deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting participant: {ex.Message}");
            }
        }
    }
}
