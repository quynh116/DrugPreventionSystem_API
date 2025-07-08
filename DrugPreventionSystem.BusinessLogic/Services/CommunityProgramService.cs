using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class CommunityProgramService : ICommunityProgramService
    {
        private readonly ICommunityProgramRepository _communityProgramRepository;
        private readonly IProgramParticipantRepository _participantRepository;

        public CommunityProgramService(ICommunityProgramRepository communityProgramRepository, IProgramParticipantRepository participantRepository) 
        {
            _communityProgramRepository = communityProgramRepository;
            _participantRepository = participantRepository;

        }

        public async Task<CommunityProgramResponse> MapToResponse(CommunityProgram program)
        {
            if(program == null)
            {
                return null;
            }
            var count = await _participantRepository.CountByProgramIdAsync(program.ProgramId);

            return new CommunityProgramResponse
            {
                ProgramId = program.ProgramId,
                Title = program.Title,
                Description = program.Description,
                TargetAudience = program.TargetAudience,
                StartDate = program.StartDate,
                EndDate = program.EndDate,
                Location = program.Location,
                CreatedAt = program.CreatedAt,
                UpdatedAt = program.UpdatedAt,
                MaxParticipants = program.MaxParticipants,
                CurrentParticipantsCount = count
            };
        }
        public async Task<Result<CommunityProgramResponse>> AddCommunityProgram(CommunityProgramCreateRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Title))
                {
                    return Result<CommunityProgramResponse>.Invalid("Program title is required.");
                }
                if(string.IsNullOrEmpty(request.TargetAudience))
                {
                    return Result<CommunityProgramResponse>.Invalid("Target audience is required.");
                }
                if (request.StartDate >= request.EndDate)
                {
                    return Result<CommunityProgramResponse>.Invalid("Start date must be before end date.");
                }
                var communityProgram = new CommunityProgram
                {
                    ProgramId = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Description,
                    TargetAudience = request.TargetAudience,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Location = request.Location,
                    CreatedAt = DateTime.Now,
                    MaxParticipants = request.MaxParticipants,
                    SurveyId = request.SurveyId,
                };
                var addedProgram = await _communityProgramRepository.AddCommunityProgramAsync(communityProgram);
                if (addedProgram == null)
                {
                    return Result<CommunityProgramResponse>.Error("Failed to add community program.");
                }
                return Result<CommunityProgramResponse>.Success(await MapToResponse(addedProgram), "Community program added successfully.");
            }
            catch(Exception ex)
            {
                return Result<CommunityProgramResponse>.Error($"Error adding community program: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteCommunityProgram(Guid communityProgramId)
        {
            try
            {
                var existingProgram = await _communityProgramRepository.GetProgramByIdAsync(communityProgramId);
                if(existingProgram == null)
                {
                    return Result<bool>.NotFound("Community program not found.");
                }
                await _communityProgramRepository.DeleteCommunityProgramAsync(communityProgramId);
                return Result<bool>.Success(true, "Community program deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting community program: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CommunityProgramResponse>>> GetAllPrograms()
        {
            try
            {
                var programs = await _communityProgramRepository.GetAllProgramsAsync();
                if (programs == null || !programs.Any())
                {
                    return Result<IEnumerable<CommunityProgramResponse>>.NotFound("No community programs found.");
                }
                var programResponses = new List<CommunityProgramResponse>();
                foreach (var program in programs)
                {
                    var response = await MapToResponse(program);
                    programResponses.Add(response);
                }
                return Result<IEnumerable<CommunityProgramResponse>>.Success(programResponses, "Community programs retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CommunityProgramResponse>>.Error($"Error retrieving community programs: {ex.Message}");
            }
        }

        public async Task<Result<CommunityProgramResponse>> GetProgramById(Guid id)
        {
            try
            {
                var program = await _communityProgramRepository.GetProgramByIdAsync(id);
                if (program == null)
                {
                    return Result<CommunityProgramResponse>.NotFound("Community program not found.");
                }
                return Result<CommunityProgramResponse>.Success(await MapToResponse(program), "Community program retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<CommunityProgramResponse>.Error($"Error retrieving community program: {ex.Message}");
            }
        }

        public async Task<Result<CommunityProgramResponse>> UpdateCommunityProgram(CommunityProgramUpdateRequest program, Guid programId)
        {
            try
            {
                var existingProgram = await _communityProgramRepository.GetProgramByIdAsync(programId);
                if (existingProgram == null)
                {
                    return Result<CommunityProgramResponse>.NotFound("Community program not found.");
                }
                if (string.IsNullOrEmpty(program.Title))
                {
                    return Result<CommunityProgramResponse>.Invalid("Program title is required.");
                }
                if (string.IsNullOrEmpty(program.TargetAudience))
                {
                    return Result<CommunityProgramResponse>.Invalid("Target audience is required.");
                }
                if (program.StartDate >= program.EndDate)
                {
                    return Result<CommunityProgramResponse>.Invalid("Start date must be before end date.");
                }
                existingProgram.Title = program.Title;
                existingProgram.Description = program.Description;
                existingProgram.TargetAudience = program.TargetAudience;
                existingProgram.StartDate = program.StartDate;
                existingProgram.EndDate = program.EndDate;
                existingProgram.Location = program.Location;
                existingProgram.UpdatedAt = DateTime.Now;
                existingProgram.MaxParticipants = program.MaxParticipants;
                var updatedProgram = await _communityProgramRepository.UpdateCommunityProgramAsync(existingProgram);
                return Result<CommunityProgramResponse>.Success(await MapToResponse(updatedProgram), "Community program updated successfully.");
            }
            catch (Exception ex)
            {
                return Result<CommunityProgramResponse>.Error($"Error updating community program: {ex.Message}");
            }
        }
    }
}
