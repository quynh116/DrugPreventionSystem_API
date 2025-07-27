using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class AdminAnalyticsService : IAdminAnalyticsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseEnrollmentRepository _userCourseEnrollmentRepository;
        private readonly IUserSurveyResponseRepository _userSurveyResponseRepository;
        private readonly IProgramParticipantRepository _programParticipantRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICommunityProgramRepository _communityProgramRepository;
        private readonly IBlogRepository _blog;

        public AdminAnalyticsService(
            IUserRepository userRepository,
            IUserCourseEnrollmentRepository userCourseEnrollmentRepository,
            IUserSurveyResponseRepository userSurveyResponseRepository,
            IProgramParticipantRepository programParticipantRepository, ICourseRepository courseRepository, ICommunityProgramRepository communityProgramRepository, IBlogRepository blog)
        {
            _userRepository = userRepository;
            _userCourseEnrollmentRepository = userCourseEnrollmentRepository;
            _userSurveyResponseRepository = userSurveyResponseRepository;
            _programParticipantRepository = programParticipantRepository;
            _courseRepository = courseRepository;
            _communityProgramRepository = communityProgramRepository;
            _blog = blog;
        }
        public async Task<Result<AdminDashboardSummaryDto>> GetAdminDashboardSummaryAsync()
        {
            try
            {
                var summary = new AdminDashboardSummaryDto();

                const int memberRoleId = 5; 
                summary.TotalUsers = await _userRepository.CountUsersByRoleIdAsync(memberRoleId);

                var allEnrollments = await _userCourseEnrollmentRepository.GetAllAsync();
                summary.TotalCourseEnrollments = allEnrollments.Count();

                var allSurveyResponses = await _userSurveyResponseRepository.GetAllAsync();
                summary.TotalCompletedSurveys = allSurveyResponses.Count();

                var allProgramParticipants = await _programParticipantRepository.GetAllAsync();
                summary.TotalProgramParticipations = allProgramParticipants.Count();

                return Result<AdminDashboardSummaryDto>.Success(summary, "Admin dashboard summary retrieved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving admin dashboard summary: {ex.Message}");
                return Result<AdminDashboardSummaryDto>.Error($"Error retrieving admin dashboard summary: {ex.Message}");
            }
            
        }
        public async Task<Result<DashboardSummaryDto>> GetDashboardSummaryAsync()
        {
            try
            {
                var summary = new DashboardSummaryDto();
                const int memberRoleId = 5;
                summary.TotalUsers = await _userRepository.CountUsersByRoleIdAsync(memberRoleId);
                var allCourses = await _courseRepository.GetAllAsync();
                summary.TotalCourses = allCourses.Count();

                var allProgram = await _communityProgramRepository.GetAllProgramsAsync();
                summary.TotalPrograms = allProgram.Count();

                var allBlogs = await _blog.GetAllAsync();
                summary.Totalblogs = allBlogs.Count();
                return Result<DashboardSummaryDto>.Success(summary, "dashboard summary retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<DashboardSummaryDto>.Error($"Error retrieving dashboard summary: {ex.Message}");
            }
        }
    }
}
