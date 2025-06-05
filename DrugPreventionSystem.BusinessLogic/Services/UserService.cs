using DrugPreventionSystem.BusinessLogic.Commons;
using DrugPreventionSystem.BusinessLogic.Models.Request;
using DrugPreventionSystem.BusinessLogic.Models.Responses;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.DataAccess.Models;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.DataAccess.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepositories)
        {
            _userRepository = userRepositories;
        }

        private UserResponse MapToUserResponse(User user)
        {
            if (user == null) return null;

            return new UserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role?.RoleName ?? "N/A",
                IsActive = user.IsActive,
                EmailVerified = user.EmailVerified,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                LastLogin = user.LastLogin
            };
        }
        public async Task<Result<bool>> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Result<bool>.NotFound($"User with ID {id} not found.");
                }

                await _userRepository.DeleteUserAsync(id);
                return Result<bool>.Success(true, "User deleted.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Error($"Error deleting user: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserResponse>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                var userResponses = users.Select(u => MapToUserResponse(u)).ToList();
                return Result<IEnumerable<UserResponse>>.Success(userResponses);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserResponse>>.Error($"Error retrieving users: {ex.Message}");
            }
        }

        public async Task<Result<UserResponse>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Result<UserResponse>.NotFound($"User with ID {id} not found.");
                }
                return Result<UserResponse>.Success(MapToUserResponse(user));
            }
            catch (Exception ex)
            {
                return Result<UserResponse>.Error($"Error retrieving user: {ex.Message}");
            }
        }

        public async Task<Result<UserResponse>> RegisterMemberAsync(UserRegistrationRequest request)
        {
            try
            {
                
                if (await _userRepository.GetUserByUsernameAsync(request.Username) != null)
                {
                    return Result<UserResponse>.Duplicated("Username already exists.");
                }
                if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                {
                    return Result<UserResponse>.Duplicated("Email already exists.");
                }

                // Lấy vai trò "Member"
                var memberRole = await _userRepository.GetRoleByNameAsync("Member");
                if (memberRole == null)
                {
                    return Result<UserResponse>.Error("Member role not found.");
                }

                var newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = PasswordHasher.HashPassword(request.Password),
                    RoleId = memberRole.RoleId,
                    IsActive = true,
                    EmailVerified = false,
                    CreatedAt = DateTime.Now
                };

                var addedUser = await _userRepository.AddUserAsync(newUser);
                var userWithRole = await _userRepository.GetUserByIdAsync(addedUser.UserId);
                return Result<UserResponse>.Success(MapToUserResponse(userWithRole), "Member registered.");
            }
            catch (Exception ex)
            {
                return Result<UserResponse>.Error($"Error during registration: {ex.Message}");
            }
        }

        public async Task<Result<UserResponse>> UpdateUserAsync(Guid id, UserUpdateRequest request)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return Result<UserResponse>.NotFound($"User with ID {id} not found.");
                }

                // Cập nhật các thuộc tính nếu được cung cấp
                if (!string.IsNullOrEmpty(request.Username) && user.Username != request.Username)
                {
                    if (await _userRepository.GetUserByUsernameAsync(request.Username) != null)
                    {
                        return Result<UserResponse>.Duplicated("Username already exists.");
                    }
                    user.Username = request.Username;
                }

                if (!string.IsNullOrEmpty(request.Email) && user.Email != request.Email)
                {
                    if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                    {
                        return Result<UserResponse>.Duplicated("Email already exists.");
                    }
                    user.Email = request.Email;
                    user.EmailVerified = false;
                }

                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.PasswordHash = PasswordHasher.HashPassword(request.Password);
                }

                if (request.IsActive.HasValue)
                {
                    user.IsActive = request.IsActive.Value;
                }

                if (request.EmailVerified.HasValue)
                {
                    user.EmailVerified = request.EmailVerified.Value;
                }

                if (request.RoleId.HasValue && user.RoleId != request.RoleId.Value)
                {
                    var roleExists = await _userRepository.GetRoleByIdAsync(request.RoleId.Value);
                    if (roleExists != null)
                    {
                        user.RoleId = request.RoleId.Value;
                    }
                    else
                    {
                        return Result<UserResponse>.Invalid("Invalid role ID.");
                    }
                }

                user.UpdatedAt = DateTime.Now;
                await _userRepository.UpdateUserAsync(user);

                // Load lại user với Role để MapToUserResponse không bị lỗi null
                var updatedUserWithRole = await _userRepository.GetUserByIdAsync(user.UserId);
                return Result<UserResponse>.Success(MapToUserResponse(updatedUserWithRole), "User updated.");
            }
            catch (Exception ex)
            {
                return Result<UserResponse>.Error($"Error updating user: {ex.Message}");
            }
        }
    }
}
