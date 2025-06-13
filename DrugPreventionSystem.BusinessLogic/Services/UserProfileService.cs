using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services
{
    public class UserProfileService : IUserProfileService
    {

        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        private UserProfileResponse MapToUserProfileResponse(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return null;
            }

            return new UserProfileResponse
            {
                ProfileId = userProfile.ProfileId,
                UserId = userProfile.UserId,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                DateOfBirth = userProfile.DateOfBirth,
                Gender = userProfile.Gender,
                PhoneNumber = userProfile.PhoneNumber,
                Address = userProfile.Address,
                City = userProfile.City,
                Occupation = userProfile.Occupation,
                EducationLevel = userProfile.EducationLevel,
                AgeGroup = userProfile.AgeGroup,
                AvatarUrl = userProfile.AvatarUrl,
                CreatedAt = userProfile.CreatedAt,
                UpdatedAt = userProfile.UpdatedAt
            };
        }
        public async Task<Result<bool>> DeleteUserProfileAsync(Guid id)
        {
            try
            {
                var user = await _userProfileRepository.GetUserProfileByIdAsync(id);
                if (user == null)
                {
                    return Result<bool>.NotFound($"User with ID {id} not found.");
                }
                await _userProfileRepository.DeleteUserProfileAsync(id);
                return Result<bool>.Success(true, "User Profile Deleted.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting user profile: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserProfileResponse>>> GetAllUsersProfileAsync()
        {
            try
            {
                var userProfiles = await _userProfileRepository.GetAllUsersProfileAsync();
                var userProfileResponse = userProfiles.Select(up => MapToUserProfileResponse(up)).ToList();
                return Result<IEnumerable<UserProfileResponse>>.Success(userProfileResponse);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserProfileResponse>>.Error($"Error retrieving user profile: {ex.Message}");
            }
        }

        public async Task<Result<UserProfileResponse>> GetUserProfileByIdAsync(Guid id)
        {
            try
            {
                var userProfile = await _userProfileRepository.GetUserProfileByIdAsync(id);
                if (userProfile == null)
                {
                    return Result<UserProfileResponse>.NotFound($"User with ID {id} not found.");
                }
                return Result<UserProfileResponse>.Success(MapToUserProfileResponse(userProfile));
            }
            catch (Exception ex)
            {
                return Result<UserProfileResponse>.Error($"Error retrieving user: {ex.Message}");
            }
        }

        public async Task<Result<UserProfileResponse>> UpdateProfileUserAsync(Guid id, UserProfileUpdateRequest request)
        {
            try
            {
                var userProfile = await _userProfileRepository.GetUserProfileByIdAsync(id);
                if (userProfile == null)
                {
                    return Result<UserProfileResponse>.NotFound($"User with ID {id} not found.");
                }

                if (!string.IsNullOrEmpty(request.FirstName) && userProfile.FirstName != request.FirstName)
                {
                    userProfile.FirstName = request.FirstName;
                }

                if (!string.IsNullOrEmpty(request.LastName) && userProfile.LastName != request.LastName)
                {
                    userProfile.LastName = request.LastName;
                }

                userProfile.DateOfBirth = request.DateOfBirth;

                if (!string.IsNullOrEmpty(request.Gender) && userProfile.Gender != request.Gender)
                {
                    userProfile.Gender = request.Gender;
                }

                if (!string.IsNullOrEmpty(request.Address) && userProfile.Address != request.Address)
                {
                    userProfile.Address = request.Address;
                }

                if (!string.IsNullOrEmpty(request.City) && userProfile.Address != request.City)
                {
                    userProfile.City = request.City;
                }

                if (!string.IsNullOrEmpty(request.Occupation) && userProfile.Address != request.Occupation)
                {
                    userProfile.Occupation = request.Occupation;
                }

                if (!string.IsNullOrEmpty(request.EducationLevel) && userProfile.Address != request.EducationLevel)
                {
                    userProfile.EducationLevel = request.EducationLevel;
                }

                if (!string.IsNullOrEmpty(request.AgeGroup) && userProfile.Address != request.AgeGroup)
                {
                    userProfile.AgeGroup = request.AgeGroup;
                }

                if (!string.IsNullOrEmpty(request.AvatarUrl) && userProfile.Address != request.AvatarUrl)
                {
                    userProfile.AvatarUrl = request.AvatarUrl;
                }

                userProfile.UpdatedAt = DateTime.Now;

                await _userProfileRepository.UpdateUserProfileAsync(userProfile);

                return Result<UserProfileResponse>.Success(MapToUserProfileResponse(userProfile), "User Profile Updated!");

            }
            catch (Exception ex)
            {
                return Result<UserProfileResponse>.Error($"Error updating user: {ex.Message}");
            }
        }

        public async Task<Result<UserProfileResponse>> GetUserProfileByUserIdAsync(Guid userId)
        {
            try
            {
                var userProfile = await _userProfileRepository.GetUserProfileByUserIdAsync(userId);
                if (userProfile == null)
                {
                    return Result<UserProfileResponse>.NotFound($"User with ID {userId} not found.");
                }
                return Result<UserProfileResponse>.Success(MapToUserProfileResponse(userProfile));
            }
            catch (Exception ex)
            {
                return Result<UserProfileResponse>.Error($"Error retrieving user: {ex.Message}");
            }
        }
    }
}