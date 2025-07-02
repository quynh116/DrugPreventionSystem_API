using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramParticipant;
using DrugPreventionSystem.BusinessLogic.Models.Request.ProgramFeedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Models.Responses.CommunityProgram;
using DrugPreventionSystem.BusinessLogic.Models.Responses.ProgramFeedback;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramService : IProgramService
    {
        private readonly ICommunityProgramRepository _programRepository;
        private readonly IProgramParticipantRepository _participantRepository;
        private readonly IProgramFeedbackRepository _feedbackRepository;
        private readonly IUserRepository _userRepository;

        public ProgramService(
            ICommunityProgramRepository programRepository,
            IProgramParticipantRepository participantRepository,
            IProgramFeedbackRepository feedbackRepository,
            IUserRepository userRepository)
        {
            _programRepository = programRepository;
            _participantRepository = participantRepository;
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
        }

        // 1. Đăng ký tham gia chương trình
        public async Task<ProgramParticipantResponse> RegisterForProgramAsync(ProgramParticipantCreateRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            if (user == null) throw new ArgumentException("User not found.");

            var program = await _programRepository.GetProgramByIdAsync(request.ProgramId);
            if (program == null) throw new ArgumentException("Program not found.");

            // Kiểm tra số lượng người tham gia tối đa
            if (program.MaxParticipants.HasValue)
            {
                var currentParticipantsCount = await _participantRepository.CountByProgramIdAsync(request.ProgramId);
                if (currentParticipantsCount >= program.MaxParticipants.Value)
                {
                    throw new InvalidOperationException("Program is full. Cannot register for this program.");
                }
            }

            var existing = (await _participantRepository.GetAllAsync())
                .FirstOrDefault(p => p.UserId == request.UserId && p.ProgramId == request.ProgramId);
            if (existing != null)
                throw new InvalidOperationException("User is already registered for this program.");

            var participant = new ProgramParticipant
            {
                ParticipantId = Guid.NewGuid(),
                UserId = request.UserId,
                ProgramId = request.ProgramId,
                RegisteredAt = DateTime.Now,
                Attended = false,
                FeedbackSubmitted = false
            };

            await _participantRepository.CreateAsync(participant);
            return MapToResponse(participant);
        }

        private ProgramParticipantResponse MapToResponse(ProgramParticipant participant)
        {
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

        // 2. Lấy các chương trình mà người dùng đã đăng ký
        public async Task<IEnumerable<CommunityProgramResponse>> GetProgramsUserEnrolledAsync(Guid userId)
        {
            var enrolled = (await _participantRepository.GetAllAsync())
                .Where(p => p.UserId == userId)
                .Select(p => p.ProgramId)
                .Distinct()
                .ToList();

            var programs = new List<CommunityProgramResponse>();
            foreach (var programId in enrolled)
            {
                var program = await _programRepository.GetProgramByIdAsync(programId);
                if (program != null)
                    programs.Add(MapToCommunityProgramResponse(program));
            }
            return programs;
        }

        private CommunityProgramResponse MapToCommunityProgramResponse(CommunityProgram program)
        {
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
                UpdatedAt = program.UpdatedAt
            };
        }

        // 3. Gửi feedback sau khi tham gia
        public async Task<ProgramFeedbackResponse> SubmitProgramFeedbackAsync(ProgramFeedbackCreateRequest request)
        {
            var participant = (await _participantRepository.GetAllAsync())
                .FirstOrDefault(p => p.UserId == request.UserId && p.ProgramId == request.ProgramId);

            if (participant == null)
                throw new InvalidOperationException("User is not registered for this program.");

            var program = await _programRepository.GetProgramByIdAsync(request.ProgramId);
            if (program == null)
                throw new InvalidOperationException("Program not found.");

            if (participant.FeedbackSubmitted)
                throw new InvalidOperationException("Feedback already submitted for this program.");

            if (DateTime.Now <= program.EndDate)
                throw new InvalidOperationException("Cannot submit feedback. Program has not ended yet.");

            var feedback = new ProgramFeedback
            {
                FeedbackId = Guid.NewGuid(),
                UserId = request.UserId,
                ProgramId = request.ProgramId,
                Rating = request.Rating,
                Comments = request.Comments,
                SubmittedAt = DateTime.Now
            };
            await _feedbackRepository.AddAsync(feedback);

            participant.Attended = true;
            participant.FeedbackSubmitted = true;
            await _participantRepository.UpdateAsync(participant);

            return MapToFeedbackResponse(feedback);
        }

        private ProgramFeedbackResponse MapToFeedbackResponse(ProgramFeedback feedback)
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