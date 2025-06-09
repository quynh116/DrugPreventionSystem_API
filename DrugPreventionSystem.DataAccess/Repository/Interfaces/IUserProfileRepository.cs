using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugPreventionSystem.DataAccess.Models;

namespace DrugPreventionSystem.DataAccess.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> GetAllUsersProfileAsync();
        Task<UserProfile?> GetUserProfileByIdAsync(Guid id);
        Task<UserProfile?> GetUserProfileByEmailAsync(string email);
        Task<UserProfile?> GetUserProfileByUsernameAsync(string username);
        Task<UserProfile> AddUserProfileAsync(UserProfile userProfile);
        Task UpdateUserProfileAsync(UserProfile userProfile);
        Task DeleteUserProfileAsync(Guid id);
        //Task DeleteUserProfileAsync(Guid id);

        //getAll, getById, them profile, sua profile, delete profile
        //users, user_profiles, user_course_enrollments, appointments, user_survey_responses, user_survey_answers, notifications, user_activities
    }
}
