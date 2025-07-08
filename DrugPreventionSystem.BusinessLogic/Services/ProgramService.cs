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
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.BusinessLogic.Models.Request.CommunityProgram;
using Microsoft.EntityFrameworkCore;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class ProgramService : IProgramService
    {
        private readonly ICommunityProgramRepository _programRepository;
        private readonly IProgramParticipantRepository _participantRepository;
        private readonly IProgramFeedbackRepository _feedbackRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProgramSurveyRepository _surveyRepository;
        private readonly IProgramSurveyResponseRepository _surveyResponseRepository;
        private readonly IProgramSurveyQuestionRepository _surveyQuestionRepository;
        private readonly IProgramSurveyAnswerRepository _surveyAnswerRepository;
        private readonly IProgramSurveyAnswerOptionRepository _surveyAnswerOptionRepository;

        public ProgramService(
            ICommunityProgramRepository programRepository,
            IProgramParticipantRepository participantRepository,
            IProgramFeedbackRepository feedbackRepository,
            IUserRepository userRepository,
            IProgramSurveyRepository surveyRepository,
            IProgramSurveyResponseRepository surveyResponseRepository,
            IProgramSurveyQuestionRepository surveyQuestionRepository,
            IProgramSurveyAnswerRepository surveyAnswerRepository,
            IProgramSurveyAnswerOptionRepository surveyAnswerOptionRepository)
        {
            _programRepository = programRepository;
            _participantRepository = participantRepository;
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
            _surveyRepository = surveyRepository;
            _surveyResponseRepository = surveyResponseRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyAnswerRepository = surveyAnswerRepository;
            _surveyAnswerOptionRepository = surveyAnswerOptionRepository;
        }

        private async Task<ProgramSurveyResponseDto> MapSurveyResponseToDto(ProgramSurveyResponse response)
        {
            var dto = new ProgramSurveyResponseDto
            {
                ResponseId = response.ResponseId,
                SubmittedAt = response.SubmittedAt,
                Answers = new List<ProgramSurveyAnswerDto1>()
            };

            foreach (var answer in response.Answers)
            {
                var question = await _surveyQuestionRepository.GetByIdAsync(answer.QuestionId);
                string? optionText = null;

                if (answer.SelectedOptionId.HasValue)
                {
                    var option = await _surveyAnswerOptionRepository.GetByIdAsync(answer.SelectedOptionId.Value);
                    optionText = option?.OptionText;
                }

                dto.Answers.Add(new ProgramSurveyAnswerDto1
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    QuestionType = question.QuestionType,
                    AnswerText = answer.AnswerText,
                    SelectedOptionId = answer.SelectedOptionId,
                    SelectedOptionText = optionText
                });
            }

            return dto;
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
                    programs.Add(await MapToCommunityProgramResponse(program));
            }
            return programs;
        }

        private async Task<CommunityProgramResponse> MapToCommunityProgramResponse(CommunityProgram program)
        {
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

        public async Task<bool> CancelRegistrationAsync(ProgramParticipantCancelRequest request)
        {
            var participant = (await _participantRepository.GetAllAsync())
                .FirstOrDefault(p => p.UserId == request.UserId && p.ProgramId == request.ProgramId);
            if (participant == null)
            {
                throw new InvalidOperationException("User are not register for this program");
            }
            await _participantRepository.DeleteAsync(participant.ParticipantId);
            return true;
        }

        public async Task<ProgramSurveyWithUserResponseDto?> GetProgramSurveyAsync(Guid programId)
        {
            var program = await _programRepository.GetProgramDetailsByIdAsync(programId);
            if (program == null)
            {
                throw new ArgumentException("Program not found.");
            }

            if (!program.SurveyId.HasValue || program.ProgramSurvey == null)
            {
                return null; 
            }

            var survey = program.ProgramSurvey; 

            var resultDto = new ProgramSurveyWithUserResponseDto
            {
                SurveyId = survey.SurveyId,
                Title = survey.Title,
                Description = survey.Description,
                Questions = survey.Questions.Select(q => new ProgramSurveyQuestionDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    AnswerOptions = q.AnswerOptions.Select(ao => new ProgramSurveyAnswerOptionDto
                    {
                        OptionId = ao.OptionId,
                        OptionText = ao.OptionText
                    }).ToList()
                }).ToList()
            };

            
            


            return resultDto;
        }

        public async Task<bool> CanUserTakeSurveyAsync(Guid userId, Guid programId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            var program = await _programRepository.GetProgramDetailsByIdAsync(programId); 
            if (program == null || !program.SurveyId.HasValue)
            {
                return false; 
            }

            // Người dùng phải là người tham gia chương trình
            var participant = await GetUserProgramParticipationStatusAsync(userId, programId);
            if (participant == null)
            {
                return false; 
            }

            // Chương trình phải kết thúc
            if (DateTime.Now <= program.EndDate)
            {
                return false; 
            }

            

            return true;

        }

        public async Task<ProgramSurveyResponseDto> SubmitProgramSurveyAsync(Guid userId, Guid programId, SubmitProgramSurveyDto surveyDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId)
                ?? throw new ArgumentException("User not found.");

            var program = await _programRepository.GetProgramDetailsByIdAsync(programId)
                ?? throw new ArgumentException("Program not found.");

            if (!program.SurveyId.HasValue)
                throw new InvalidOperationException("This program does not have a survey.");

            var canTakeSurvey = await CanUserTakeSurveyAsync(userId, programId);
            if (!canTakeSurvey)
                throw new InvalidOperationException("Cannot submit survey. Either the program has not ended, you are not registered, or you have already submitted the survey.");

            var survey = await _surveyRepository.GetByIdAsync(program.SurveyId.Value)
                ?? throw new InvalidOperationException("Associated survey not found.");

            var surveyResponse = new ProgramSurveyResponse
            {
                ResponseId = Guid.NewGuid(),
                SurveyId = survey.SurveyId,
                ProgramId = programId,
                UserId = userId,
                SubmittedAt = DateTime.Now,
                Answers = new List<ProgramSurveyAnswer>()
            };

            foreach (var answerDto in surveyDto.Answers)
            {
                var question = await _surveyQuestionRepository.GetByIdAsync(answerDto.QuestionId);
                if (question == null || question.SurveyId != survey.SurveyId)
                    throw new ArgumentException($"Question {answerDto.QuestionId} not found or does not belong to this survey.");

                var answer = new ProgramSurveyAnswer
                {
                    AnswerId = Guid.NewGuid(),
                    ResponseId = surveyResponse.ResponseId,
                    QuestionId = answerDto.QuestionId,
                    AnswerText = answerDto.AnswerText,
                    SelectedOptionId = answerDto.SelectedOptionId
                };

                surveyResponse.Answers.Add(answer);
            }

            await _surveyResponseRepository.AddAsync(surveyResponse);

            return await MapSurveyResponseToDto(surveyResponse);
        }


        public async Task<ProgramSurveyResponseDto?> GetUserProgramSurveyResponseAsync(Guid userId, Guid programId)
        {
            var program = await _programRepository.GetProgramDetailsByIdAsync(programId);
            if (program == null || !program.SurveyId.HasValue)
                return null;

            var response = await _surveyResponseRepository
                .GetByUserIdProgramIdAndSurveyIdWithAnswersAsync(userId, programId, program.SurveyId.Value);

            if (response == null) return null;

            return await MapSurveyResponseToDto(response);
        }


        public async Task<ProgramParticipant?> GetUserProgramParticipationStatusAsync(Guid userId, Guid programId)
        {
            return await _participantRepository.GetByUserIdAndProgramIdAsync(userId, programId);
        }
    }
}